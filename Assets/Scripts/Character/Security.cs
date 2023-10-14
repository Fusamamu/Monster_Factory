using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Monster
{
    public class Security : MonoBehaviour, ICharacter
    {
        public bool IsInit { get; private set; }

        public bool IsVisible { get; private set; } = true;
        

        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [field: SerializeField] public Transform TargetTransform { get; private set; }
        
        [SerializeField] private RenderControl    RenderControl;
        [SerializeField] private CameraController CameraController;
        
        [SerializeField] private Collider VisibleCollider;

        private Camera mainCam;
        
        private static readonly int isRunning = Animator.StringToHash("IsRunning");

        private List<IVisible> allVisibleInRange = new List<IVisible>();

        public void Init()
        {
            if(IsInit)
                return;
            IsInit = true;
            
            RenderControl.Init();

            mainCam = Camera.main;
        }
        
        public void SetVisible(bool _value)
        {
            IsVisible = _value;
            gameObject.SetActive(_value);
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                var _ray = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (Physics.Raycast(_ray, out var _mouseHit, Mathf.Infinity))
                {
                    Animator.SetBool(isRunning, true);   
                    NavMeshAgent.destination = _mouseHit.point;
                }
            }
            
            if (!NavMeshAgent.pathPending)
            {
                if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
                    if (!NavMeshAgent.hasPath || NavMeshAgent.velocity.sqrMagnitude == 0f)
                        Animator.SetBool(isRunning, false);
            }
            
            var _position = TargetTransform.position;
            RenderControl.SetLightPosition(_position);
            CameraController.AnchorTarget.position = _position;
        }

        private void OnTriggerEnter(Collider _other)
        {
            if (_other.TryGetComponent<IVisible>(out var _visible))
            {
                _visible.SetVisible(true);
                allVisibleInRange.Add(_visible);
            }
        }

        private void OnTriggerExit(Collider _other)
        {
            foreach (var _visible in allVisibleInRange)
                _visible.SetVisible(false);
        }
    }
}
