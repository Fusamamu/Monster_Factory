using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shapes;
using UnityEngine;

namespace Monster
{
    public enum ZoneType
    {
        GOAL, ROOM
    }

    [Serializable]
    public class SubZoneRef
    {
        public string Name;
        public Vector3 ZonePos;
    }
    
    public class ZoneDetector : MonoBehaviour
    {
        [field: SerializeField] public ZoneType ZoneType { get; private set; }

        public List<SubZoneRef> AllSubZoneRefs = new List<SubZoneRef>();

        private void OnTriggerEnter(Collider _other)
        {
            if (_other.TryGetComponent<Security>(out var _security))
            {
                foreach (var _scientist in _security.AllFollowScientists)
                {
                    
                    // _scientist.OnStopFollowHandler();
                    // _scientist.MoveTo(transform.position + AllSubZoneRefs.First().ZonePos);
                }
            }
        }

        private void OnDrawGizmos()
        {
            foreach (var _subZone in AllSubZoneRefs)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(transform.position + _subZone.ZonePos, 0.25f);
            }
        }
    }
}
