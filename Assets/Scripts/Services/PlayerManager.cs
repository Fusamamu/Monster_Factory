using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class PlayerManager : Service
    {
        [field: SerializeField] public Security SelectedPlayer { get; private set; }

        [SerializeField] private int ControlIndex;
        [SerializeField] private RenderControl RenderControl;
        
        private readonly Dictionary<SecurityType, Security> securityTable = new Dictionary<SecurityType, Security>();

        private PlayerControl playerControl;
        private CameraManager cameraManager;
        private Coroutine changeControlProcess;

        public override void Init()
        {
            if(IsInit)
                return;
            IsInit = true;
            
            RenderControl.Init();

            cameraManager = ServiceLocator.Instance.Get<CameraManager>();

            var _allSecurities = FindObjectsOfType<Security>();

            foreach (var _security in _allSecurities)
            {
                if (!securityTable.ContainsKey(_security.SecurityType))
                    securityTable.Add(_security.SecurityType, _security);
            }

            changeControlProcess = StartCoroutine(SelectPlayerCoroutine(SecurityType.BRIGHT));

            playerControl = new PlayerControl();
            playerControl.PlayerSelection.Enable();
            playerControl.PlayerSelection.ChangePlayerControl.performed += _context =>
            {
                if (changeControlProcess != null)
                {
                    StopCoroutine(changeControlProcess);
                    changeControlProcess = null;
                }
                
                ControlIndex = (int)SelectedPlayer.SecurityType + 1;
                
                if (ControlIndex > 3)
                    ControlIndex = 0;

                var _nextPlayerType = (SecurityType)ControlIndex;
                
                changeControlProcess = StartCoroutine(SelectPlayerCoroutine(_nextPlayerType));
            };
        }

        public IEnumerator SelectPlayerCoroutine(SecurityType _securityType)
        {
            foreach (var _security in securityTable.Values)
                _security.OnEndBeingControlled();
            
            if(!securityTable.TryGetValue(_securityType, out var _targetPlayer))
                yield break;

            yield return RenderControl.ScaleLightDown();

            yield return cameraManager.MoveAnchorToCoroutine(_targetPlayer.transform.position, _pos =>
            {
                RenderControl.SetLightPosition(_pos);
            });
            
            yield return RenderControl.ScaleLightUp();
            
            SelectedPlayer = _targetPlayer;
            SelectedPlayer.OnStartBeingControlled();
            
            ServiceLocator.Instance
                .Get<UIManager>()
                .Get<CharacterDisplayGUI>()
                .OnCharacterControlChanged(SelectedPlayer);
        }
    }
}
