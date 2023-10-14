using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace Monster
{
    public class Security : MonoBehaviour, ICharacter
    {
        public bool IsInit { get; private set; }

        public bool IsVisible { get; private set; } = true;
        
        public bool IsTargetLock { get; set; }

        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

        private Vector3 destinationPos;
        
        [field: SerializeField] public Transform TargetTransform { get; private set; }
        [field: SerializeField] public Transform ShootTarget     { get; private set;  }
        
        [SerializeField] private RenderControl    RenderControl;
        [SerializeField] private CameraController CameraController;

        /*Shooting setting*/
        [SerializeField] private bool IsShooting;
        [SerializeField] private float ShootInterval;
        [SerializeField] private BulletPool BulletPool;
        [SerializeField] private Transform BulletSpawnTarget;
        private Coroutine shootingProcess;

        private readonly List<IVisible>   allVisibleInRange   = new List<IVisible>();
        private readonly List<IShootAble> allShootAbleInRange = new List<IShootAble>();
        
        // private static readonly int isAnimatorRunning  = Animator.StringToHash("IsRunning");
        // private static readonly int isAnimatorShooting = Animator.StringToHash("IsShooting");

        private Camera mainCam;
        
        public void Init()
        {
            if(IsInit)
                return;
            IsInit = true;
            
            BulletPool.Init();
            RenderControl.Init();

            mainCam = Camera.main;
        }
        
        public void SetVisible(bool _value)
        {
            IsVisible = _value;
            gameObject.SetActive(_value);
        }

        private void Update()
        {
            if(!IsInit)
                return;
            
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                StopShooting();
                
                var _ray = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (Physics.Raycast(_ray, out var _mouseHit, Mathf.Infinity))
                {
                    Animator.SetBool(AnimHash.IsRunning, true);
                    NavMeshAgent.isStopped = false;
                    NavMeshAgent.destination = _mouseHit.point;
                }
            }
            
            /*Stop move*/
            if (!NavMeshAgent.pathPending)
            {
                if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
                    if (!NavMeshAgent.hasPath || NavMeshAgent.velocity.sqrMagnitude == 0f)
                        Animator.SetBool(AnimHash.IsRunning, false);
            }
            
            var _position = TargetTransform.position;
            RenderControl.SetLightPosition(_position);
            
            if(CameraController)
                CameraController.AnchorTarget.position = _position;

            if (IsShooting)
            {
                foreach (var _visible in allVisibleInRange)
                {
                    if (_visible is IShootAble _shootAble)
                    {
                        if(_shootAble.IsTargetLock)
                            continue;
                        
                        _shootAble.IsTargetLock = true;
                         
                        shootingProcess = StartCoroutine(ShootBulletCoroutine(_shootAble.ShootTarget.position));
                    }
                }
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

            foreach (var _shootAble in allShootAbleInRange)
                _shootAble.IsTargetLock = false;
            allVisibleInRange.Clear();
        }

        private void OnTriggerEnter(Collider _other)
        {
            if (_other.TryGetComponent<IVisible>(out var _visible))
            {
                _visible.SetVisible(true);

                if (_visible is IShootAble _shootAble)
                {
                    StartShooting();

                    NavMeshAgent.ResetPath();
                    NavMeshAgent.isStopped = true;
                    
                    if(!allShootAbleInRange.Contains(_shootAble))
                        allShootAbleInRange.Add(_shootAble);
                }
                
                if(!allVisibleInRange.Contains(_visible))
                    allVisibleInRange.Add(_visible);
            }

            if (_other.TryGetComponent<Scientist>(out var _scientist))
            {
                _scientist.IsFound   = true;
                _scientist.TargetPos = TargetTransform.position;
                _scientist.FollowTarget = TargetTransform;
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
    }
}
