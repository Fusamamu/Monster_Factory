using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Monster
{
    [Serializable]
    public class CameraPositionUpdateEvent : UnityEvent <Vector3> {}
    
    public class CameraController : MonoBehaviour, IInitializable
    {
        public bool IsInit { get; private set; }
        
        [field: SerializeField] public Camera    MainCamera   { get; private set; }
        [field: SerializeField] public Transform AnchorTarget { get; private set; }

        [SerializeField] private float CameraSpeed;

        private CameraControl cameraControl;

        [SerializeField] public CameraPositionUpdateEvent CameraPositionUpdateEvent = new ();
        
        public void Init()
        {
            if(IsInit)
                return;
            IsInit = true;
            
            cameraControl = new CameraControl();
            cameraControl.CameraMovement.Enable();

            cameraControl.CameraMovement.MoveLeft .performed += MoveLeft;
            cameraControl.CameraMovement.MoveRight.performed += MoveRight;
            cameraControl.CameraMovement.MoveUp   .performed += MoveUp;
            cameraControl.CameraMovement.MoveDown .performed += MoveDown;
        }

        public void SetAnchorTarget(Transform _target)
        {
            AnchorTarget = _target;
        }

        private void Update()
        {
            var _a = cameraControl.CameraMovement.MoveLeft.ReadValue<float>();
            if (_a > 0f) 
                AnchorTarget.transform.position += Vector3.left * CameraSpeed * _a;

            var _b = cameraControl.CameraMovement.MoveRight.ReadValue<float>();
            if (_b > 0f)
                AnchorTarget.transform.position += Vector3.right * CameraSpeed * _b;

            var _zoomValue = cameraControl.CameraMovement.ZoomIn.ReadValue<float>();
            if (_zoomValue > 0f)
                MainCamera.transform.position += MainCamera.transform.forward * CameraSpeed;
            
            var _zoomOutValue = cameraControl.CameraMovement.ZoomOut.ReadValue<float>();
            if (_zoomOutValue > 0f)
                MainCamera.transform.position += -(MainCamera.transform.forward) * CameraSpeed;
            
            CameraPositionUpdateEvent?.Invoke(AnchorTarget.transform.position);
        }

        private void MoveLeft(InputAction.CallbackContext _context)
        {
           // AnchorTarget.transform.position += Vector3.left * CameraSpeed;
        }
        
        private void MoveRight(InputAction.CallbackContext _context)
        {
           // AnchorTarget.transform.position += Vector3.right * CameraSpeed;
        }
        
        private void MoveUp(InputAction.CallbackContext _context)
        {
           // AnchorTarget.transform.position += Vector3.forward * CameraSpeed;
        }
        
        private void MoveDown(InputAction.CallbackContext _context)
        {
           // AnchorTarget.transform.position += Vector3.back * CameraSpeed;
        }

        private void ZoomIn(InputAction.CallbackContext _context)
        {
            
        }

        private void ZoomOut(InputAction.CallbackContext _context)
        {
            
        }
    }
}
