using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;


namespace Monster
{
    public class Scientist : MonoBehaviour, ICharacter
    {
        public bool IsInit { get; private set; }

        public bool IsVisible { get; private set; } = false;
        
        public bool IsTargetLock { get; set; }
        
        [field: SerializeField] public RenderControl RenderControl { get; private set; }
        
        [field: SerializeField] public Animator     Animator        { get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent    { get; private set; }
        public int HP { get => hp; private set => hp = value; }

        [SerializeField] private int hp;
        [SerializeField] private BehaviorTree BehaviourTree;


        public bool IsFound;
        public Transform FollowTarget;
        public Vector3 TargetPos;
        private Quaternion targetRotation;

        [SerializeField] private float rotationSpeed;

        private bool isRunning;
        
        public void Init()
        {
            if(IsInit)
                return;
            IsInit = true;

            //SetVisible(false);
            
            BehaviourTree = new BehaviorTreeBuilder(gameObject)
                //.Selector()
                    .Sequence()
                    .Condition(("Can Follow Target"), () =>
                    {
                        if (!IsFound)
                            return false;
                          
                        var _dist = Vector3.Distance(transform.position, FollowTarget.position);
                        if (_dist < 2.5f)
                            return false;

                        return true;
                    })
                    .Do("Start Follow", () =>
                    {
                        Debug.Log("Start Follow ");
                        isRunning = true;
                        Animator.SetBool(AnimHash.IsRunning, true);
                        NavMeshAgent.isStopped = false;
                        NavMeshAgent.SetDestination(FollowTarget.position + Vector3.back * 2.5f);
                        
                        return TaskStatus.Success;
                    })
                    .Do("Follow Target", () =>
                    {
                        if (isRunning)
                            return TaskStatus.Continue;
                        
                        return TaskStatus.Success;
                    })
                    .End()
                //.End()
                .Build();
            
            
            

            
               // BehaviourTree = new BehaviorTreeBuilder(gameObject)
               //  .Sequence()
               //      .Sequence()
               //      .Do("A", () =>
               //      {
               //          Debug.Log("A");
               //          return TaskStatus.Continue;
               //      })
               //      .Do("B", () =>
               //      {
               //          Debug.Log("B");
               //          return TaskStatus.Success;
               //      })
               //      .End()
               //  .Do("C", () =>
               //  {
               //      Debug.Log("C");
               //      return TaskStatus.Success;
               //  })
               //  .End()
               //  .Build();

        }
        
        private void Update () 
        {
            BehaviourTree.Tick();
            
            if (!NavMeshAgent.pathPending)
            {
                if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
                    if (!NavMeshAgent.hasPath || NavMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        isRunning = false;
                        Animator.SetBool(AnimHash.IsRunning, false);
                    }
            }
        }

        public void SetVisible(bool _value)
        {
            IsVisible = _value;
            if (!_value)
            {
                RenderControl.UseInvisibleMaterial();
                RenderControl.SetTransparent(0);
            }
            else
            {
                RenderControl.UseDefaultMaterial();
            }
        }

        public void OnFoundHandler(Transform _foundObject)
        {
            TargetPos    = _foundObject.position;
            FollowTarget = _foundObject;
            StartCoroutine(CelebrateCoroutine());
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
                    
            Animator.SetBool(AnimHash.IsCelebrate, true);
            IsFound = true;
        }
    }
}
