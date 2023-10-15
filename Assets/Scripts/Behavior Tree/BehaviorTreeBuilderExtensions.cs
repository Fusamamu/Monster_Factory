using CleverCrow.Fluid.BTs.Trees;
using UnityEngine;
using UnityEngine.AI;

namespace Monster
{
    public static class BehaviorTreeBuilderExtensions
    {
        public static BehaviorTreeBuilder AgentRoaming (this BehaviorTreeBuilder _builder, string _name, NavMeshAgent _agent, Transform _transform, float _range)
        {
            return _builder.AddNode(new AgentRoaming
            {
                Name = _name,
                Agent = _agent,
                AgentTransform = _transform,
                Range = _range,
            });
        }

        public static BehaviorTreeBuilder AgentChasing (this BehaviorTreeBuilder _builder, string _name, NavMeshAgent _agent, Transform _targetTransform)
        {
            return _builder.AddNode(new AgentChasing
            {
                Name = _name,
                Agent = _agent,
                TargetTransform = _targetTransform,
            });
        }
    }
}
