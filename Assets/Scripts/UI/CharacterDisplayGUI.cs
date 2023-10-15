using System;
using System.Collections;
using System.Collections.Generic;
using MPUIKIT;
using UnityEngine;
using UnityEngine.UI;

namespace Monster
{
    [Serializable]
    public class RenderTextureInfo
    {
        public SecurityType SecurityType;
        public Material RTMaterial;
    }
    
    public class CharacterDisplayGUI : GUI
    {
        public List<RenderTextureInfo> RenderTextureInfos = new List<RenderTextureInfo>();

        private Dictionary<SecurityType, Material> rtTable = new Dictionary<SecurityType, Material>();

        public Image MainImage;
        public List<Image> SubImages = new List<Image>();

        public override void Init()
        {
            if(IsInit)
                return;

            IsInit = true;

            foreach (var _info in RenderTextureInfos)
            {
                if(!rtTable.ContainsKey(_info.SecurityType))
                    rtTable.Add(_info.SecurityType, _info.RTMaterial);
            }
        }

        public void OnCharacterControlChanged(Security _security)
        {
            var _index = 0;
            
            foreach (var _kvp in rtTable)
            {
                var _type = _kvp.Key;
                var _mat = _kvp.Value;

                if (_security.SecurityType == _type)
                {
                    MainImage.material = _mat;
                    continue;
                }

                SubImages[_index].material = _mat;
                _index++;
            }
        }
    }
}
