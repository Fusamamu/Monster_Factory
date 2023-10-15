using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using UnityEngine;
using UnityEngine.AI;

namespace Monster
{
    public class AgentRoaming : ActionBase
    {
        public NavMeshAgent Agent;

        public float Range;

        public Transform AgentTransform;

        protected override TaskStatus OnUpdate()
        {
            if (RandomPoint(AgentTransform.position, Range, out var _point))
            {
                Agent.SetDestination(_point);
                Debug.DrawRay(_point, Vector3.up, Color.blue, 1.0f);
            }

            return TaskStatus.Success;
        }

        private bool RandomPoint(Vector3 _center, float _range, out Vector3 _result)
        {
            Vector3 _randomPoint = _center + Random.insideUnitSphere * _range;

            if (NavMesh.SamplePosition(_randomPoint, out var _hit, 1.0f, NavMesh.AllAreas))
            {
                _result = _hit.position;
                return true;
            }

            _result = Vector3.zero;
            return false;
        }
    }
}
