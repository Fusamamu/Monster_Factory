using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Monster
{
    public class Grappler : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Rigidbody rigidBody;

        [Header("Settings")]
        [SerializeField] private float grapplingRange = 30;
        [SerializeField] private float grapplingForce = 25;

        [Header("Debug")]
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] private Rigidbody targetRigidBody;
        [SerializeField] private MouseHeld currentMouseHeld;

        private bool isGrappling;

        private enum MouseHeld
        {
            Left,Right
        }

        private void Update()
        {
            //these input checking will be change to events when implementing new input system

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                GetTargetTransformPosition();
            }

            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                isGrappling = true;

                if (Input.GetMouseButton(0))
                    currentMouseHeld = MouseHeld.Left;
                else
                    currentMouseHeld = MouseHeld.Right;

            }
            else
                isGrappling = false;

        }

        private void FixedUpdate()
        {
            if (isGrappling && targetPosition != null)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;

                if (currentMouseHeld == MouseHeld.Left)
                    rigidBody.AddForce(direction * grapplingForce);
                else if (currentMouseHeld == MouseHeld.Right && targetRigidBody != null)
                {
                    targetRigidBody.AddForce(-direction * grapplingForce);
                }
            }
        }

        private void GetTargetTransformPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var mouseHit, Mathf.Infinity))
            {
                if (Physics.Raycast(transform.position, mouseHit.point-transform.position, out var grapplingHit))
                {
                    targetPosition = grapplingHit.point;
                    targetRigidBody = grapplingHit.rigidbody;

                    Debug.Log(grapplingHit.transform.name);

                }

            }
        }

        #region Gizmos
        private void OnDrawGizmos()
        {
            if (currentMouseHeld == MouseHeld.Left)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.blue;

            if (targetPosition != null && isGrappling)
                Gizmos.DrawLine(transform.position, targetPosition);
        }

        #endregion
    }
}
