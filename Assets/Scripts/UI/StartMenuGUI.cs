using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monster
{
    public class StartMenuGUI : MonoBehaviour
    {
        public Button StartMenuButton;
        public Button QuitButton;
        
        public void Start()
        {
            StartMenuButton.onClick.AddListener(() =>
            {
                GlobalSceneManager.Instance.LoadNewGameScene("PrototypeScene");
                
            });
            
            QuitButton.onClick.AddListener(Application.Quit);
        }
    }
}
