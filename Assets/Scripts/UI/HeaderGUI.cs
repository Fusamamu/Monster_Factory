using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

namespace Monster
{
    public class HeaderGUI : GUI
    {
        public MMF_Player OnOpenAnimation;

        public TextMeshProUGUI MainText;
        public TextMeshProUGUI SubTest;

        public override void Init()
        {
            if(IsInit)
                return;
            IsInit = true;
            
            OnOpenAnimation.Initialization();
        }

        public override void Open()
        {
            if(OnOpenAnimation.IsPlaying)
                return;
            
            OnOpenAnimation.Direction = MMFeedbacks.Directions.TopToBottom;
            OnOpenAnimation.PlayFeedbacks();

            CloseAfterSeconds(3f);
        }

        public override void Close()
        {
            if(OnOpenAnimation.IsPlaying)
                return;
            
            OnOpenAnimation.Direction = MMFeedbacks.Directions.BottomToTop;
            OnOpenAnimation.PlayFeedbacks();
        }

        private void CloseAfterSeconds(float _sec)
        {
            StartCoroutine(CloseCoroutine(_sec));
        }

        private IEnumerator CloseCoroutine(float _sec)
        {
            yield return new WaitForSeconds(_sec);
            
            Close();
        }
    }
}
