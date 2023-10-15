using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Monster
{
    public class Monster : MonoBehaviour, ICharacter, IAttackAble
    {
        public bool IsInit { get; private set; }

        public bool IsVisible { get; private set; } = false;

        public bool IsTargetLock { get; set; }
        
        public Transform AttackTarget { get; }

        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

        [field: SerializeField] public int HP { get; private set; }

        [SerializeField] private BehaviorTree BehaviourTree;

        public bool IsFound;
        public Transform FollowTarget;
        public Vector3 TargetPos;
        private Quaternion targetRotation;

        [SerializeField] private float rotationSpeed;

        public void Init()
        {
            if (IsInit)
                return;
            IsInit = true;


            BehaviourTree = new BehaviorTreeBuilder(gameObject)
                .Selector()
                    .Sequence("Chasing")
                        .Condition(() => FollowTarget != null)
                        .Do("Chase Target", () => 
                        {
                            NavMeshAgent.SetDestination(FollowTarget.transform.position);
                            return TaskStatus.Success;
                        })
                    .End()
                .AgentRoaming("Roaming", NavMeshAgent, transform, 5)
                .End()
                .Build();
        }
        
        public void ReceiveDamage(int _damage)
        {
        }

        private void Update()
        {
            BehaviourTree.Tick();
        }

        public void SetVisible(bool _value)
        {
            //IsVisible = _value;
            //if (!_value)
            //{
            //    RenderControl.UseInvisibleMaterial();
            //    RenderControl.SetTransparent(0);
            //}
            //else
            //{
            //    RenderControl.UseDefaultMaterial();
            //}
        }
    }
}
