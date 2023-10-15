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
        
        public void Init()
        {
            if(IsInit)
                return;
            IsInit = true;

            SetVisible(false);
            
            BehaviourTree = new BehaviorTreeBuilder(gameObject)
                .Sequence()
                .Do("Wait to be found", () =>
                {
                    if (!IsFound)
                        return TaskStatus.Continue;
                    
                    Vector3 _directionToTarget = TargetPos - transform.position;
                     
                    targetRotation = Quaternion.LookRotation(_directionToTarget);
                    
                    while (Quaternion.Angle(transform.rotation, targetRotation) > 0.5f)
                    {
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                        return TaskStatus.Continue;
                    }
                    
                    Animator.SetBool(AnimHash.IsCelebrate, true);
                  
                    return TaskStatus.Success;
                })
                .WaitTime("Wait", 3f)
                .Do("Follow", () =>
                {
                    if (FollowTarget)
                    {
                        if (Vector3.Distance(transform.position, FollowTarget.position) > 5f)
                        {
                            Debug.Log("Start Run");
                            Animator.SetBool(AnimHash.IsRunning, true);
                            NavMeshAgent.isStopped = false;
                            NavMeshAgent.SetDestination(FollowTarget.position + Vector3.back * 2);
                        }
                        else
                        {
                            Debug.Log("Stop Run");
                            Animator.SetBool(AnimHash.IsRunning, false);
                            NavMeshAgent.isStopped = true;
                        }
                        
                        if (!NavMeshAgent.pathPending)
                        {
                            Debug.Log("Pending");
                            if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
                                if (!NavMeshAgent.hasPath || NavMeshAgent.velocity.sqrMagnitude == 0f)
                                    Animator.SetBool(AnimHash.IsRunning, false);
                        }
                        
                        return TaskStatus.Success;
                    }
                    
                    return TaskStatus.Continue;
                })
                .End()
                .Build();
        }
        
        private void Update () 
        {
            BehaviourTree.Tick();
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
    }
}
