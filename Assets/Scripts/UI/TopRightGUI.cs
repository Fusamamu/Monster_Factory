using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monster
{
    public class TopRightGUI : GUI
    {
        public Button MenuButton;
        
        public override void Init()
        {
            if(IsInit)
                return;

            IsInit = true;
            
            MenuButton.onClick.AddListener(() =>
            {
                ServiceLocator.Instance
                    .Get<UIManager>()
                    .Get<MenuPanel>().Open();
            });
        }
    }
}
