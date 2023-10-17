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
        public Transform ZoneTransform;
    }
    
    public class ZoneDetector : MonoBehaviour
    {
        [field: SerializeField] public ZoneType ZoneType { get; private set; }

        public List<SubZoneRef> AllSubZoneRefs = new List<SubZoneRef>();

        private void OnTriggerEnter(Collider _other)
        {
            if (_other.TryGetComponent<Security>(out var _security))
                _security.OnEnterZoneDetector(this);
        }

        private void OnDrawGizmos()
        {
            foreach (var _subZone in AllSubZoneRefs)
            {
                if(_subZone.ZoneTransform == null)
                    continue;
                
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(_subZone.ZoneTransform.position, 0.25f);
            }
        }
    }
}
