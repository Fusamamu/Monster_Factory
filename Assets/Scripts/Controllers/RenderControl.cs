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

        [SerializeField] private float LightScaleSpeed = 10f;

        private MaterialPropertyBlock propertyBlock;
        private Coroutine alphaProcess;
        private float alphaValue;
        [SerializeField] private float AlphaSpeed = 5f;

        public void Init()
        {
            if(IsInit)
                return;
            IsInit = true;
            
            propertyBlock = new MaterialPropertyBlock();
        }

        public void UseDefaultMaterial()
        {
            TargetRenderer.sharedMaterial = TargetMaterial;
        }
        
        public void UseInvisibleMaterial()
        {
            TargetRenderer.sharedMaterial = InvisibleMaterial;
        }

        public void ToggleOnTransparent(Action _onComplete)
        {
            if (alphaProcess != null)
            {
                StopCoroutine(alphaProcess);
                alphaProcess = null;
            }
            alphaProcess = StartCoroutine(TransparentOnCoroutine(_onComplete));
        }

        public void ToggleOffTransparent()
        {
            if (alphaProcess != null)
            {
                StopCoroutine(alphaProcess);
                alphaProcess = null;
            }
            alphaProcess = StartCoroutine(TransparentOffCoroutine());
        }

        private IEnumerator TransparentOnCoroutine(Action _onComplete)
        {
            while (alphaValue < 1)
            {
                alphaValue += Time.deltaTime * AlphaSpeed;
                SetTransparent(alphaValue);
                yield return null;
            }
            alphaValue = 1;
            SetTransparent(1);
            _onComplete?.Invoke();
        }
        
        private IEnumerator TransparentOffCoroutine()
        {
            while (alphaValue > 0)
            {
                alphaValue -= Time.deltaTime * AlphaSpeed;
                SetTransparent(alphaValue);
                yield return null;
            }

            alphaValue = 0;
            SetTransparent(0);
        }

        public void SetTransparent(float _value)
        {
            //InvisibleMaterial.color = new Color(1, 1, 1, _value);
            propertyBlock.SetColor("_Color", new Color(1, 1, 1, _value));
            TargetRenderer.SetPropertyBlock (propertyBlock);
        }

        public void SetLightPosition(Vector3 _pos)
        {
            TargetMaterial.SetVector(samplePos, _pos);
        }

        public void MoveLightPositionTo(Vector3 _pos)
        {
            
        }

        public IEnumerator ScaleLightDown()
        {
            var _currentValue = MainRadiusValue;
            
            var _targetValue = 0.2f;
            
            while (_currentValue > _targetValue)
            {
                _currentValue -= Time.deltaTime * LightScaleSpeed;
                TargetMaterial.SetFloat(mainRadius, _currentValue);
                TargetMaterial.SetFloat(secondaryRadius, _currentValue + 0.5f);
                yield return null;
            }
            
            TargetMaterial.SetFloat(mainRadius, _targetValue);
            TargetMaterial.SetFloat(secondaryRadius, _targetValue + 0.5f);
        }

        public IEnumerator ScaleLightUp()
        {
            var _currentValue = 0.2f;

            var _targetValue = MainRadiusValue;
            
            while (_currentValue < _targetValue)
            {
                _currentValue += Time.deltaTime * LightScaleSpeed;
                TargetMaterial.SetFloat(mainRadius, _currentValue);
                TargetMaterial.SetFloat(secondaryRadius, _currentValue + 0.5f);
                yield return null;
            }
            
            TargetMaterial.SetFloat(mainRadius, _targetValue);
            TargetMaterial.SetFloat(secondaryRadius, _targetValue + 0.5f);
        }

        private void Update()
        {
            if (TargetMaterial)
            {
                TValue += Time.deltaTime;

                if (TValue > 1)
                    TValue = 0;

                var _mainValue      = MainRadiusValue      * RadiusAnimation.Evaluate(TValue);
                var _secondaryValue = SecondaryRadiusValue * RadiusAnimation.Evaluate(TValue);
                
                TargetMaterial.SetFloat(mainRadius     , _mainValue);
                TargetMaterial.SetFloat(secondaryRadius, _secondaryValue);
            }
        }
    }
}
