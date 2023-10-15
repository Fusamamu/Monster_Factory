using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class ColliderControl : MonoBehaviour, IInitializable
    {
        public bool IsInit { get; private set; }
        
        [field: SerializeField] public List<Collider> Colliders { get; private set; }

        public void Init()
        {
            if(!IsInit)
                return;
            IsInit = true;
        }

        public void EnableAllColliders()
        {
            foreach (var _collider in Colliders)
                _collider.enabled = true;
        }

        public void DisableAllColliders()
        {
            foreach (var _collider in Colliders)
                _collider.enabled = false;
        }
    }
}
