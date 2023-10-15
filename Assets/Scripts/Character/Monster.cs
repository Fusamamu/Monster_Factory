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

        //[field: SerializeField] public RenderControl RenderControl { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

        public Transform AttackTarget { get; }

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
            if (IsInit)
                return;
            IsInit = true;

            //RenderControl.Init();

            BehaviourTree = new BehaviorTreeBuilder(gameObject)
                .Selector()
                    .AgentChasing("Chasing", NavMeshAgent, FollowTarget)
                    .AgentRoaming("Roaming", NavMeshAgent, transform, 5)
                .End()
                .Build();
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
