using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using MimicSpace;
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

        public bool IsDead { get; set; }
        
        public bool IsTargetLock { get; set; }

        public Transform AttackTarget { get; private set; }

        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [field: SerializeField] public RenderControl RenderControl { get; private set; }

        [field: SerializeField] public int HP { get; private set; }

        [SerializeField] private BehaviorTree BehaviourTree;

        public bool IsFound;
        public Transform FollowTarget;
        public Vector3 TargetPos;
        private Quaternion targetRotation;

        [SerializeField] private float rotationSpeed;
        [SerializeField] private FieldOfView fieldOfView;
        [SerializeField] private AttackController attackController;
        [SerializeField] private Mimic mimicControl;

        public void Init()
        {
            if (IsInit)
                return;
            IsInit = true;
            
            RenderControl.Init();

            AttackTarget = transform;

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
                            var _attackTarget = FollowTarget.GetComponent(typeof(IDamageable)) as IDamageable;

                            if (_attackTarget is null)
                                return TaskStatus.Failure;

                            attackController.AttackTarget(this, _attackTarget);
                            return TaskStatus.Success;
                        })
                    .End()
                .AgentRoaming("Roaming", NavMeshAgent, transform, 100)
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

            float _distance = (fieldOfView.VisibleTarget[0].transform.position - transform.position).magnitude;

            Transform _nearestTargetTransform = null;

            foreach(var _target in fieldOfView.VisibleTarget)
            {
                ICharacter _character = _target.GetComponent(typeof(ICharacter)) as ICharacter;

                if(_character is null || _character.IsDead)
                    continue;

                var _newDistance = (_target.transform.position - transform.position).magnitude;

                if (_distance > _newDistance)
                {
                    _distance = _newDistance;
                    _nearestTargetTransform = _target;
                }
            }

            FollowTarget = _nearestTargetTransform;
            
            if (FollowTarget != null)
                Debug.DrawLine(transform.position, FollowTarget.transform.position, Color.red);
        }

        public void Attack(IDamageable _attackTarget, int _damage) 
        {
            Debug.DrawLine(transform.position, FollowTarget.transform.position, Color.yellow);
            mimicControl.RequestLeg(FollowTarget.transform.position, mimicControl.legResolution, attackController.AttackRange,mimicControl.minGrowCoef, mimicControl, attackController.AttackDelay);
            _attackTarget.ReceiveDamage(_damage);
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
