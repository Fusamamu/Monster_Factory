using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Shapes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Monster
{
    public class Grappler : MonoBehaviour, IInitializable
    {
        public bool IsInit { get; private set; }

        [SerializeField] private Line      LineRender;
        [SerializeField] private Rigidbody RigidBody;

        [SerializeField] private float GrapplingRange = 30;
        [SerializeField] private float GrapplingForce = 25;

        [SerializeField] private Rigidbody TargetRigidBody;
        [SerializeField] private Vector3 TargetPosition;
        
        [SerializeField] private MouseHeld CurrentMouseHeld;

        private Camera mainCamera;
        
        private bool isGrappling;

        private enum MouseHeld
        {
            Left,Right
        }

        public void Init()
        {
            if(IsInit)
                return;
            IsInit = true;
            
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame || 
                Mouse.current.rightButton.wasPressedThisFrame)
                GetTargetTransformPosition();

            if (Mouse.current.leftButton.isPressed)
            {
                isGrappling = true;   
                CurrentMouseHeld = MouseHeld.Left;
            }

            if (Mouse.current.rightButton.isPressed)
            {
                isGrappling = true;
                CurrentMouseHeld = MouseHeld.Right;
            }

            LineRender.End = TargetPosition - transform.position;
        }

        private void FixedUpdate()
        {
            if (!isGrappling) return;
            
            Vector3 _direction = (TargetPosition - transform.position).normalized;

            switch (CurrentMouseHeld)
            {
                case MouseHeld.Left:
                    RigidBody.AddForce(_direction * GrapplingForce);
                    break;
                case MouseHeld.Right when TargetRigidBody != null:
                    TargetRigidBody.AddForce(-_direction * GrapplingForce);
                    break;
            }
        }

        private void GetTargetTransformPosition()
        {
            var _ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            
            if (Physics.Raycast(_ray, out var _mouseHit, Mathf.Infinity))
            {
                TargetPosition  = _mouseHit.point;
                TargetRigidBody = _mouseHit.rigidbody;
            }
        }

        #region Gizmos
        private void OnDrawGizmos()
        {
            if (CurrentMouseHeld == MouseHeld.Left)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.blue;

            if (isGrappling)
                Gizmos.DrawLine(transform.position, TargetPosition);
        }
        #endregion
    }
}
