using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Monster
{
    public class Player : MonoBehaviour, IInitializable
    {
        public bool IsInit { get; private set; }

        [field: SerializeField] public Transform TargetTransform { get; private set; }
        
        [SerializeField] private Grappler Grappler;

        [SerializeField] private RenderControl    RenderControl;
        [SerializeField] private CameraController CameraController;
        
        public void Init()
        {
            if(IsInit)
                return;
            IsInit = true;
            
            Grappler     .Init();
            RenderControl.Init();
        }

        private void Update()
        {
            var _position = TargetTransform.position;
            RenderControl.SetLightPosition(_position);
            CameraController.AnchorTarget.position = _position;
        }
    }
}
