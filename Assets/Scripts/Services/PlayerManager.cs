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
        
        public readonly Dictionary<SecurityType, Security> SecurityTable = new Dictionary<SecurityType, Security>();

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
                if (!SecurityTable.ContainsKey(_security.SecurityType))
                    SecurityTable.Add(_security.SecurityType, _security);
            }

            changeControlProcess = StartCoroutine(SelectPlayerCoroutine(SecurityType.BRIGHT));

            playerControl = new PlayerControl();
            playerControl.PlayerSelection.Enable();

            playerControl.PlayerSelection.ChangePlayerIndexUp.performed += _context =>
            {
                if (changeControlProcess != null)
                {
                    StopCoroutine(changeControlProcess);
                    changeControlProcess = null;

                    ControlIndex = (int)(SelectedPlayer.SecurityType + 1) % 4;

                    var _nextPlayerType = (SecurityType)ControlIndex;

                    changeControlProcess = StartCoroutine(SelectPlayerCoroutine(_nextPlayerType));
                }
            };

            playerControl.PlayerSelection.ChangePlayerIndexDown.performed += _context =>
            {
                if (changeControlProcess != null)
                {
                    StopCoroutine(changeControlProcess);
                    changeControlProcess = null;

                    ControlIndex = (int)(SelectedPlayer.SecurityType + 3) % 4;

                    var _nextPlayerType = (SecurityType)ControlIndex;

                    changeControlProcess = StartCoroutine(SelectPlayerCoroutine(_nextPlayerType));
                }
            };
        }

        public IEnumerator SelectPlayerCoroutine(SecurityType _securityType)
        {
            foreach (var _security in SecurityTable.Values)
                _security.OnEndBeingControlled();
            
            if(!SecurityTable.TryGetValue(_securityType, out var _targetPlayer))
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

        public void OnSecurityDeadHandler(Security _security)
        {
            var _allDead = true;
            
            foreach (var _sec in SecurityTable.Values)
            {
                if (!_sec.IsDead)
                {
                    _allDead = false;
                    break;
                }
            }

            if (_allDead)
                ServiceLocator
                    .Instance.Get<UIManager>()
                    .Get<SummaryGUI>().Open();
        }
    }
}
