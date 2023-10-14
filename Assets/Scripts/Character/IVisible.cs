using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public interface IVisible
    {
        public bool IsVisible { get; }
        public void SetVisible(bool _value);
    }
}
