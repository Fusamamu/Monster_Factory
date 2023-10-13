using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class ApplicationStarter : MonoBehaviour
    {
        [SerializeField] private Player Player;
        [SerializeField] private CameraController CameraController;

        private void Start()
        {
            Player.Init();
            CameraController.Init();
            
        }
    }
}
