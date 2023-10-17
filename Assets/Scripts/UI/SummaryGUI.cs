using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Monster
{
    public class SummaryGUI : GUI
    {
        public MMF_Player OnOpenAnimation;
        public MMF_Player OnCloseAnimation;

        public TextMeshProUGUI BrightStatusText;
        public TextMeshProUGUI PoomStatusText;
        public TextMeshProUGUI KameeStatusText;
        public TextMeshProUGUI AiStatusText;

        private Dictionary<SecurityType, TextMeshProUGUI> statusTable = new Dictionary<SecurityType, TextMeshProUGUI>();

        public Button ViewStageButton;
        public Button ReplayButton;

        private PlayerManager playerManager;

        public override void Init()
        {
            if(IsInit)
                return;

            IsInit = true;

            OnOpenAnimation.Initialization();
            OnCloseAnimation.Initialization();

            playerManager = ServiceLocator.Instance.Get<PlayerManager>();
            
            statusTable.Add(SecurityType.BRIGHT, BrightStatusText);
            statusTable.Add(SecurityType.POOM  , PoomStatusText);
            statusTable.Add(SecurityType.KAMEE , KameeStatusText);
            statusTable.Add(SecurityType.AI    , AiStatusText);
            
            ReplayButton.onClick.AddListener(() =>
            {
                GlobalSceneManager.Instance.LoadScene("PrototypeScene");
            });
        }

        public override void Open()
        {
            OnOpenAnimation.PlayFeedbacks();

            foreach (var _kvp in playerManager.SecurityTable)
            {
                var _type     = _kvp.Key;
                var _security = _kvp.Value;

                if (statusTable.TryGetValue(_type, out var _text))
                {
                    if(_security.IsDead)
                        _text.SetText("DECEASE");
                    else
                        _text.SetText("SURVIVE");
                }
            }
        }
        
        public override void Close()
        {
            OnCloseAnimation.PlayFeedbacks();
        }
    }
}
