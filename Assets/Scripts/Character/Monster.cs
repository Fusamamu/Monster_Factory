using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

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
        [SerializeField] private FieldOfView fieldOfView;
        [SerializeField] private AttackController attackController;

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
                        .Condition(() => (FollowTarget.position - transform.position).magnitude <= attackController.AttackRange)
                        .Do("Attack Target", () =>
                        {
                            var attackTarget = FollowTarget.GetComponent(typeof(IDamageable)) as IDamageable;

                            if (attackTarget is null)
                                return TaskStatus.Failure;

                            attackController.AttackTarget(this, attackTarget);
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
            GetNearestTarget();
            BehaviourTree.Tick();
        }

        public void GetNearestTarget()
        {
            if (fieldOfView.VisibleTarget.Count == 0)
            {
                FollowTarget = null;
                return;
            }

            float distance = (fieldOfView.VisibleTarget[0].transform.position - transform.position).magnitude;

            Transform nearestTargetTransform = fieldOfView.VisibleTarget[0];

            foreach(var target in fieldOfView.VisibleTarget)
            {
                if (target.GetComponent<ICharacter>() is null)
                {
                    nearestTargetTransform = null;
                    continue;
                }

                var newDistance = (target.transform.position - transform.position).magnitude;

                if (distance > newDistance)
                    nearestTargetTransform = target;

            }

            FollowTarget = nearestTargetTransform;

            if (FollowTarget != null)
                Debug.DrawLine(transform.position, FollowTarget.transform.position, Color.red);
        }

        public void Attack(IDamageable _attackTarget, int _damage) 
        {
            //_attackTarget.ReceiveDamage(_damage);
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
