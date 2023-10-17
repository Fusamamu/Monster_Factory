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
        [SerializeField] private LayerMask ignoreLayerMask;
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

                if (Physics.Raycast(transform.position, collider.transform.position - transform.position, out var hit, viewRadius))
                {
                    Debug.DrawLine(transform.position, collider.transform.position, Color.green);

                    //if (!((1 << hit.transform.gameObject.layer) == targetLayerMask.value))
                    //{
                    //    Debug.Log("obstacle");
                    //    continue;
                    //}

                    if (!visibleTarget.Contains(collider.transform))
                    {
                        visibleTarget.Add(collider.transform);
                    }
                }
            }
        }

    }
}
