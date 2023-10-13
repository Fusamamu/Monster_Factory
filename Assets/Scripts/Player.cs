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
        [SerializeField] private CameraController CameraController;
        
        public void Init()
        {
            if(IsInit)
                return;
            IsInit = true;
            
            Grappler.Init();
        }

        private void Update()
        {
            CameraController.AnchorTarget.position = TargetTransform.position;
        }
    }
}
