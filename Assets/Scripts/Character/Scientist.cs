using System;
using System.Collections;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;

namespace Monster
{
    public class Scientist : MonoBehaviour, ICharacter
    {
        public bool IsInit { get; private set; }

        public bool IsSafe { get; set; }
        
        public bool IsDead { get; set; }
        
        public bool IsVisible { get; private set; } = false;
        
        [field:SerializeField] public int HP { get; private set; }
        
        [field: SerializeField] public bool IsTargetLock { get; set; }
        
        [field: SerializeField] public RenderControl RenderControl { get; private set; }
        
        [field: SerializeField] public Animator     Animator        { get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent    { get; private set; }
      
        [SerializeField] private BehaviorTree BehaviourTree;

        public bool IsFound;
        public Security FollowSecurity;
        public Vector3 TargetPos;
        private Quaternion targetRotation;

        [SerializeField] private float rotationSpeed;

        
        
        private float coolDown;
        private bool isFinishCelebrated;
        private bool canStartFollow = true;
        private bool isFollowing;

        private float stopDistance  = 1.5f;
        private float breakDistance = 3.0f;
        
        public void Init()
        {
            if(IsInit)
                return;
            IsInit = true;
            
            RenderControl.Init();
            
            //SetVisible(false);

            NavMeshAgent.stoppingDistance = 1.5f;
        }
        
        private void Update () 
        {
            if(IsDead)
                return;
            
            if (IsSafe)
            {
                if (!NavMeshAgent.pathPending)
                {
                    if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
                        if (!NavMeshAgent.hasPath || NavMeshAgent.velocity.sqrMagnitude == 0f)
                            Animator.SetBool(AnimHash.IsRunning, false);

                    if(!PlayerManager.MissionComplete)
                        ServiceLocator.Instance.Get<CharacterManager>().OnScientistSaved(this);
                }
                return;
            }
            
            if(FollowSecurity == null || !isFinishCelebrated)
                return;
            
            float _distanceToTarget = Vector3.Distance(transform.position, FollowSecurity.transform.position);

            if (_distanceToTarget > breakDistance)
            {
                OnStopFollowHandler();
                return;
            }
            
            if (_distanceToTarget > stopDistance)
            {
                Animator.SetBool(AnimHash.IsRunning, true);
                NavMeshAgent.isStopped = false;
                NavMeshAgent.SetDestination(FollowSecurity.transform.position);
            }
            else
            {
                Animator.SetBool(AnimHash.IsRunning, false);
                NavMeshAgent.isStopped = true;
            }
            
            Vector3 _directionToTarget = (FollowSecurity.transform.position - transform.position).normalized;
            if (_directionToTarget != Vector3.zero)
            {
                Quaternion _targetRotation = Quaternion.LookRotation(_directionToTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * NavMeshAgent.angularSpeed);
            }
        }

        public void MoveTo(Vector3 _targetPos)
        {
            Animator.SetBool(AnimHash.IsRunning, true);
            NavMeshAgent.isStopped = false;
            NavMeshAgent.SetDestination(_targetPos);
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

                var _scientistLeftCount = ServiceLocator.Instance.Get<CharacterManager>().AllScientists.Count(_o => !_o.IsDead);

                var _headerGUI = ServiceLocator.Instance.Get<UIManager>().Get<HeaderGUI>();
                _headerGUI.MainText.SetText("SCIENTIST LIFE SIGNAL LOST");
                _headerGUI.SubTest.SetText($"SCIENTIST {_scientistLeftCount} LEFT");
                _headerGUI.Open();
            }
            
            Debug.Log(gameObject.name + " recieved " + _damage + " current hp is now " + HP);
        }

        public void SetVisible(bool _value)
        {
            IsVisible = _value;
            
            if (!_value)
            {
                RenderControl.UseInvisibleMaterial();
                RenderControl.ToggleOffTransparent();
            }
            else
            {
                RenderControl.ToggleOnTransparent(() =>
                {
                    RenderControl.UseDefaultMaterial();
                });
            }
        }

        public void OnStartFollowHandler(Security _security)
        {
            if(isFinishCelebrated)
                return;
            
            FollowSecurity = _security;
            TargetPos      = _security.transform.position;
            StartCoroutine(CelebrateCoroutine());
        }

        public void OnStopFollowHandler()
        {
            if(!isFollowing)
                return;
            
            Debug.Log("Stop Follow");
                
            isFollowing        = false;
            isFinishCelebrated = false;
            FollowSecurity     = null;
            
            Animator.SetBool(AnimHash.IsRunning  , false);
            Animator.SetBool(AnimHash.IsCelebrate, false);
            Animator.SetBool(AnimHash.IsTerrified, true);
            NavMeshAgent.isStopped = true;
        }

        private IEnumerator CelebrateCoroutine()
        {
            Vector3 _directionToTarget = TargetPos - transform.position;
                     
            targetRotation = Quaternion.LookRotation(_directionToTarget);
                    
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.5f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                yield return null;
            }
                    
            IsFound = true;
            Animator.SetBool(AnimHash.IsCelebrate, true);
            Animator.SetBool(AnimHash.IsTerrified, false);
            yield return new WaitForSeconds(2.5f);

            isFinishCelebrated = true;
            isFollowing        = true;
            
            Debug.Log("Start Follow");
        }
        
        private IEnumerator WaitForStateToFinish(string _stateName)
        {
            if (Animator.HasState(0, Animator.StringToHash(_stateName)))
                yield return new WaitUntil(() => !Animator.GetCurrentAnimatorStateInfo(0).IsName(_stateName));
        }
    }
}
