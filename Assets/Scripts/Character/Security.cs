using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Monster
{
    public enum SecurityType
    {
        KAMEE = 0, 
        AI,
        BRIGHT,
        POOM
    }
    
    public class Security : MonoBehaviour, ICharacter, IAttackAble
    {
        [field: SerializeField] public SecurityType SecurityType { get; private set; }
        
        public bool IsInit    { get; private set; }
        public bool IsVisible { get; private set; } = true;
        public bool IsDead    { get; set; }
        
        [field: SerializeField] public int HP { get; private set; }
        
        public bool IsControlled { get; set; }
        public bool IsTargetLock { get; set; }

        [field: SerializeField] public Animator     Animator     { get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

        private Vector3 destinationPos;
        
        [field: SerializeField] public Transform TargetTransform { get; private set; }
        [field: SerializeField] public Transform AttackTarget    { get; private set; }

        [SerializeField] private ColliderControl ColliderControl;
        [SerializeField] private RenderControl   RenderControl;
        
        [SerializeField] private CameraManager CameraManager;

        /*Shooting setting*/
        [SerializeField] private bool IsShooting;
        [SerializeField] private float ShootInterval;
        [SerializeField] private BulletPool BulletPool;
        [SerializeField] private Transform BulletSpawnTarget;
        private Coroutine shootingProcess;

        private readonly List<IVisible> allVisibleInRange   = new List<IVisible>();

        private IAttackAble attackTarget;
        private Quaternion targetRotation;
        private Vector3 directionToTarget;
        private float rotationSpeed = 10f;
        
        private Camera mainCam;

        [field: SerializeField] public List<Scientist> AllFollowScientists { get; private set; } = new List<Scientist>();

        public ParticleSystem BloodSplatParticleSystem;
        public GameObject BloodStrainPrefab;

        public void Init()
        {
            if(IsInit)
                return;
            IsInit = true;
            
            BulletPool.Init();
            
            ColliderControl.Init();
            RenderControl  .Init();

            mainCam = Camera.main;

            CameraManager = ServiceLocator.Instance.Get<CameraManager>();
            
            ColliderControl.DisableAllColliders();
        }

        public void OnStartBeingControlled()
        {
            IsControlled = true;
            ColliderControl.EnableAllColliders();
        }

        public void OnEndBeingControlled()
        {
            IsControlled = false;
            ColliderControl.DisableAllColliders();
        }
        
        public void ReceiveDamage(int _damage)
        {
            if(IsDead)
                return;
            
            HP -= _damage;

            if (HP <= 0)
            {
                IsDead = true;
                Animator.SetBool("IsDead", true);

                ServiceLocator
                    .Instance
                    .Get<PlayerManager>()
                    .OnSecurityDeadHandler(this);
            }

            BloodSplatParticleSystem.Stop();
            BloodSplatParticleSystem.Play();

            var _blood = Instantiate(BloodStrainPrefab, transform.position, Quaternion.identity);
            _blood.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            
            GlobalAudioManager.Instance.PlayBloodSplat();
            Debug.Log(gameObject.name + " recieved " + _damage + " current hp is now " + HP);
        }
        
        public void SetVisible(bool _value)
        {
            IsVisible = _value;
            gameObject.SetActive(_value);
        }

        private void Update()
        {
            if(IsDead)
                return;
            
            if(!IsInit || !IsControlled)
                return;
            
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                StopShooting();
                
                var _ray = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (Physics.Raycast(_ray, out var _mouseHit, Mathf.Infinity, LayerMask.NameToLayer("Security"), QueryTriggerInteraction.Ignore))
                {
                    Animator.SetBool(AnimHash.IsRunning, true);
                    NavMeshAgent.isStopped = false;
                    //NavMeshAgent.destination = _mouseHit.point;
                    NavMeshAgent.SetDestination(_mouseHit.point);
                }
            }
            
            /*Stop move*/
            if (!NavMeshAgent.pathPending)
            {
                if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
                    if (!NavMeshAgent.hasPath || NavMeshAgent.velocity.sqrMagnitude == 0f)
                        Animator.SetBool(AnimHash.IsRunning, false);
                
            }
            else
            {
            }
            
            var _position = TargetTransform.position;
            RenderControl.SetLightPosition(_position);
            
            if(CameraManager)
                CameraManager.AnchorTarget.position = _position;

            if (IsShooting)
            {
                if (shootingProcess == null)
                {
                    shootingProcess   = StartCoroutine(ShootBulletCoroutine(attackTarget.AttackTarget.position));
                    directionToTarget = attackTarget.AttackTarget.position - transform.position;
                    targetRotation    = Quaternion.LookRotation(directionToTarget);
                }
                
                if(Quaternion.Angle(transform.rotation, targetRotation) > 0.5f)
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                
                if(Vector3.Distance(transform.position, attackTarget.AttackTarget.position) > 5f)
                    StopShooting();
            }
        }
        
        private IEnumerator ShootBulletCoroutine(Vector3 _targetPos)
        {
            var _shootCount = 0;
            
            while (_shootCount <= 3)
            {
                var _bullet = BulletPool.Pool?.Get();
                if (_bullet)
                {
                    _bullet
                        .SetSpawnPosition(BulletSpawnTarget.position)
                        .SetDirection(TargetTransform.forward)
                        .StartFire();
                    
                    GlobalAudioManager.Instance.PlayShooting();
                }

                yield return new WaitForSeconds(ShootInterval);
                
                _shootCount++;
            }
            
            StopShooting();
        }
        
        private void StartShooting()
        {
            IsShooting = true;
            Animator.SetBool(AnimHash.IsShooting, IsShooting);
        }

        private void StopShooting()
        {
            IsShooting = false;
            Animator.SetBool(AnimHash.IsShooting, false);

            if (shootingProcess != null)
            {
                StopCoroutine(shootingProcess);
                shootingProcess = null;
            }
        }

        public void OnEnterZoneDetector(ZoneDetector _zoneDetector)
        {
            if (_zoneDetector.ZoneType == ZoneType.GOAL)
            {
                var _target = _zoneDetector.AllSubZoneRefs.First().ZoneTransform;
                
                foreach (var _scientist in AllFollowScientists.ToList())
                {
                    if (_scientist.IsDead)
                    {
                        AllFollowScientists.Remove(_scientist);
                        continue;
                    }

                    _scientist.OnStopFollowHandler();
                    _scientist.MoveTo(_target.position);
                    _scientist.IsSafe = true;
                }
                
                AllFollowScientists.Clear();
            }
        }

        private void OnTriggerEnter(Collider _other)
        {
            if (_other.TryGetComponent<IVisible>(out var _visible))
            {
                _visible.SetVisible(true);
                
                if(!allVisibleInRange.Contains(_visible))
                    allVisibleInRange.Add(_visible);
            }
            
            if (_other.TryGetComponent<IAttackAble>(out var _attackAble))
            {
                StartShooting();
                NavMeshAgent.ResetPath();
                NavMeshAgent.isStopped = true;

                attackTarget = _attackAble;
            }

            if (_other.TryGetComponent<Scientist>(out var _scientist))
            {
                if (!_scientist.IsSafe)
                {
                    _scientist.OnStartFollowHandler(this);
                    
                    if(!AllFollowScientists.Contains(_scientist))
                        AllFollowScientists.Add(_scientist);
                }
            }
        }

        private void OnTriggerExit(Collider _other)
        {
            if (_other.TryGetComponent<IVisible>(out var _visible))
            {
                _visible.SetVisible(false);
                allVisibleInRange.Remove(_visible);
            }
        }

        public void OnBreakFromScientist(Scientist _scientist)
        {
            AllFollowScientists.Remove(_scientist);
        }
    }
}
