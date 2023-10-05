using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

namespace Monster
{
    public class ProcAnimation : MonoBehaviour
    {
        [Serializable]
        public class LegInfo
        {
            public bool IsUpdate;
            
            public Transform FootTarget;
            
            public Vector3 FootCurrentPos;
            public Vector3 Dir;
            public Vector3 DirNormal;
            
            public Line LineRender;
        }
        
        [SerializeField] private Transform BodyTransform;
        [SerializeField] private List<LegInfo> LegInfos = new List<LegInfo>();

        [SerializeField] private AnimationCurve BodyAnimation;

        [SerializeField] private float BodyHeight;
        [SerializeField] private float FootOffsetDist = 1f;
        [SerializeField] private float LegSpeed = 1f;
        [SerializeField] private float GizmosSize = 0.05f;

        private float timer;

        private void Start()
        {
            BodyHeight = Mathf.Abs(BodyTransform.position.y);

            foreach (var _info in LegInfos)
            {
                var _position = _info.FootTarget.position;
                _info.Dir       = _position - BodyTransform.position;
                _info.DirNormal = _info.Dir.normalized;
                //_info.FootCurrentPos = _position;
            }
        }

        private void Update()
        {
            var _bodyPos = BodyTransform.position;

            timer += Time.deltaTime;
            if (timer > 1.0f)
                timer = 0f;
            timer = Mathf.Clamp01(timer);
            
            var _targetHeight = BodyAnimation.Evaluate(timer);
            
            BodyTransform.position = new Vector3(_bodyPos.x, _targetHeight, _bodyPos.z);
            
            foreach (var _info in LegInfos)
            {
                if (Vector3.Distance(_info.FootTarget.position, _info.FootCurrentPos) > FootOffsetDist)
                {
                    _info.FootCurrentPos = _info.FootTarget.position;
                    _info.IsUpdate = true;
                }

                if (_info.IsUpdate)
                {
                    var _b = Vector3.MoveTowards(_info.LineRender.End, _info.Dir, LegSpeed * Time.deltaTime);
                    _info.LineRender.End = _b;

                    if (_info.LineRender.End == _info.Dir)
                        _info.IsUpdate = false;
                }
                else
                {
                    _info.LineRender.End = _info.FootCurrentPos - BodyTransform.position;
                }
            }
        }
 
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(LegInfos.Count == 0)
                return;
            
            for (var _i = 0; _i < LegInfos.Count; _i++)
            {
                var _legInfo = LegInfos[_i];

                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(_legInfo.FootTarget.position, GizmosSize);
                
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_legInfo.FootCurrentPos, GizmosSize);
            }
        }
#endif
    }
}
