using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

namespace Monster
{
    public class MenuPanel : GUI
    {
        public MMF_Player OnOpen;
        
        public Button CloseButton;
        public Button ResumeButton;
        public Button ExitButton;
        
        public override void Init()
        {
            if(IsInit)
                return;

            IsInit = true;
            
            OnOpen.Initialization();
            
            CloseButton.onClick.AddListener(Close);
            
            ResumeButton.onClick.AddListener(() =>
            {
                Close();
            });
            
            ExitButton.onClick.AddListener(() =>
            {
                GlobalSceneManager.Instance.LoadScene(SceneName.StartMenuScene);
                
                Close();
            });
        }
        
        public override void Open()
        {
            OnOpen.Direction = MMFeedbacks.Directions.TopToBottom;
            OnOpen.PlayFeedbacks();
        }

        public override void Close()
        {
            OnOpen.Direction = MMFeedbacks.Directions.BottomToTop;
            OnOpen.PlayFeedbacks();
        }
    }
}
