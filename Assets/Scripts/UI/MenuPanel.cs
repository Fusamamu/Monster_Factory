using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monster
{
    public class MenuPanel : GUI
    {
        public Button CloseButton;
        public Button ExitButton;
        
        public override void Init()
        {
            if(IsInit)
                return;

            IsInit = true;
            
            CloseButton.onClick.AddListener(Close);
            
            ExitButton.onClick.AddListener(() =>
            {
                GlobalSceneManager.Instance.LoadScene(SceneName.StartMenuScene);
            });
        }
    }
}
