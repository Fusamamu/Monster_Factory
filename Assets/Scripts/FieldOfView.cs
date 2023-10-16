using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class FieldOfView : MonoBehaviour
    {
        public float ViewRadius { get => viewRadius; }
        public List<Transform> VisibleTarget { get => visibleTarget; }

        [SerializeField] private float viewRadius;
        [SerializeField] private LayerMask targetLayerMask;
        [SerializeField] private List<Transform> visibleTarget;

        private void Update()
        {
            FindTarget();
        }

        private void FindTarget()
        {
            Collider[] targetColliders = Physics.OverlapSphere(transform.position, viewRadius);

            visibleTarget.Clear();

            foreach (Collider collider in targetColliders)
            {
                if (Physics.Raycast(transform.position, collider.transform.position - transform.position, viewRadius, targetLayerMask))
                {
                    Debug.DrawLine(transform.position, collider.transform.position, Color.green);

                    if (!visibleTarget.Contains(collider.transform))
                    {
                        visibleTarget.Add(collider.transform);
                    }
                }
            }
        }

    }
}
