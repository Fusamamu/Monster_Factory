using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class RenderControl : MonoBehaviour, IInitializable
    {
        public bool IsInit { get; private set; }

        [SerializeField] private Renderer TargetRenderer;
        [SerializeField] private Material TargetMaterial;
        [SerializeField] private Material InvisibleMaterial;
        
        private static readonly int samplePos       = Shader.PropertyToID("_SamplePos");
        private static readonly int mainRadius      = Shader.PropertyToID("_MainRadius");
        private static readonly int secondaryRadius = Shader.PropertyToID("_SecondaryRadius");

        [SerializeField] private float MainRadiusValue      = 0.4f;
        [SerializeField] private float SecondaryRadiusValue = 0.5f;

        [SerializeField] private float TValue;
        [SerializeField] private AnimationCurve RadiusAnimation;

        public void Init()
        {
            if(IsInit)
                return;
            IsInit = true;
        }

        public void UseDefaultMaterial()
        {
            TargetRenderer.sharedMaterial = TargetMaterial;
        }
        
        public void UseInvisibleMaterial()
        {
            TargetRenderer.sharedMaterial = InvisibleMaterial;
        }

        public void SetTransparent(float _value)
        {
            InvisibleMaterial.color = new Color(0, 0, 0, _value);
        }

        public void SetLightPosition(Vector3 _pos)
        {
            TargetMaterial.SetVector(samplePos, _pos);
        }

        private void Update()
        {
            if (TargetMaterial)
            {
                TValue += Time.deltaTime;

                if (TValue > 1)
                    TValue = 0;

                var _mainValue      = MainRadiusValue * RadiusAnimation.Evaluate(TValue);
                var _secondaryValue = SecondaryRadiusValue * RadiusAnimation.Evaluate(TValue);
                
                TargetMaterial.SetFloat(mainRadius     , _mainValue);
                TargetMaterial.SetFloat(secondaryRadius, _secondaryValue);
            }
        }
    }
}