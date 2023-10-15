using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using UnityEngine;
using UnityEngine.AI;

namespace Monster
{
    public class AgentChasing : ActionBase
    {
        public NavMeshAgent Agent;

        public Transform TargetTransform;

        protected override TaskStatus OnUpdate()
        {

            if (TargetTransform == null)
            {
                return TaskStatus.Failure;
            }
            else
            {
                Agent.SetDestination(TargetTransform.position);
                Debug.DrawRay(Agent.transform.position, TargetTransform.position, Color.red, 1.0f);
                return TaskStatus.Success;
            }
        }
    }
}
