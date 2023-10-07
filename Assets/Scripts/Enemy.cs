using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Monster
{
    public class Enemy : MonoBehaviour
    {
        public NavMeshAgent agent;
        
        public float range; 

        public Transform centrePoint; //centre of the area the agent wants to move around in
        //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area
    
        void Update()
        {
            if(agent.remainingDistance <= agent.stoppingDistance) //done with path
            {
                if (RandomPoint(centrePoint.position, range, out var _point)) //pass in our centre point and radius of area
                {
                    agent.SetDestination(_point);
                    Debug.DrawRay(_point, Vector3.up, Color.blue, 1.0f); 
                }
            }

        }
        bool RandomPoint(Vector3 _center, float _range, out Vector3 _result)
        {
            Vector3 _randomPoint = _center + Random.insideUnitSphere * _range; //random point in a sphere 
            
            if (NavMesh.SamplePosition(_randomPoint, out var _hit, 1.0f, NavMesh.AllAreas)) 
            { 
                //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
                //or add a for loop like in the documentation
                _result = _hit.position;
                return true;
            }

            _result = Vector3.zero;
            return false;
        }
    }
}
