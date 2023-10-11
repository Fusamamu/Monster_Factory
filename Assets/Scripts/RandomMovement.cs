using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Monster
{
	public class RandomMovement : MonoBehaviour
	{
		public NavMeshAgent Agent;
        
		public float Range; 

		public Transform CentrePoint;
    
		private void Update()
		{
			if(Agent.remainingDistance <= Agent.stoppingDistance)
			{
				if (RandomPoint(CentrePoint.position, Range, out var _point)) 
				{
					Agent.SetDestination(_point);
					Debug.DrawRay(_point, Vector3.up, Color.blue, 1.0f); 
				}
			}
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
