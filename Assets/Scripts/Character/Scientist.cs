using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Monster
{
    public class Scientist : MonoBehaviour, ICharacter
    {
        public bool IsInit { get; private set; }

        public bool IsVisible { get; private set; } = false;
        
        [field: SerializeField] public RenderControl RenderControl { get; private set; }
        
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [field: SerializeField] public Transform TargetTransform { get; private set; }
        
        public void Init()
        {
            if(IsInit)
                return;
            IsInit = true;

            SetVisible(false);
        }

        public void SetVisible(bool _value)
        {
            IsVisible = _value;
            if (!_value)
            {
                RenderControl.UseInvisibleMaterial();
                RenderControl.SetTransparent(0);
            }
            else
            {
                RenderControl.UseDefaultMaterial();
            }
        }
    }
}
