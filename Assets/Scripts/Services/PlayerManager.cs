using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class PlayerManager : Service
    {
        [field: SerializeField] public Security SelectedPlayer { get; private set; }
        
        private readonly Dictionary<SecurityType, Security> securityTable = new Dictionary<SecurityType, Security>();

        private CameraManager cameraManager;

        public override void Init()
        {
            if(IsInit)
                return;
            IsInit = true;

            cameraManager = ServiceLocator.Instance.Get<CameraManager>();

            var _allSecurities = FindObjectsOfType<Security>();

            foreach (var _security in _allSecurities)
            {
                if (!securityTable.ContainsKey(_security.SecurityType))
                    securityTable.Add(_security.SecurityType, _security);
            }

            StartCoroutine(SelectPlayerCoroutine(SecurityType.A));
        }

        public IEnumerator SelectPlayerCoroutine(SecurityType _securityType)
        {
            if(!securityTable.TryGetValue(_securityType, out var _targetPlayer))
                yield break;

            yield return cameraManager.MoveAnchorToCoroutine(_targetPlayer.transform.position);
            
            foreach (var _kvp in securityTable)
            {
                var _type     = _kvp.Key;
                var _security = _kvp.Value;
                
                if (_type == _securityType)
                {
                    _security.IsControlled = true;
                    SelectedPlayer = _security;
                    continue;
                }
                
                _security.IsControlled = false;
            }
        }
    }
}
