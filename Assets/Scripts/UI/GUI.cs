using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class GUI : MonoBehaviour, IInitializable
    {
        public bool IsInit { get; protected set; }

        public virtual void Init()
        {
            
        }
        
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
