using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Monster
{
    public class SummaryGUI : GUI
    {
        public MMF_Player OnOpenAnimation;
        public MMF_Player OnCloseAnimation;
        
        public override void Init()
        {
            if(IsInit)
                return;

            IsInit = true;

            OnOpenAnimation.Initialization();
            OnCloseAnimation.Initialization();
        }

        public override void Open()
        {
            OnOpenAnimation.PlayFeedbacks();
        }
        
        public override void Close()
        {
            OnCloseAnimation.PlayFeedbacks();
        }
    }
}
