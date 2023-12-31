using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Monster
{
    public class ApplicationStarter : MonoBehaviour
    {
        [SerializeField] private CameraManager CameraManager;

        private void Start()
        {
            var _allCharacter = FindObjectsOfType<MonoBehaviour>().OfType<ICharacter>();
            foreach (var _character in _allCharacter)
                _character.Init();
            
            if(CameraManager)
                CameraManager.Init();
        }
    }
}
