using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class AnimHash : MonoBehaviour
    {
        public static readonly int IsRunning   = Animator.StringToHash("IsRunning");
        public static readonly int IsShooting  = Animator.StringToHash("IsShooting");
        public static readonly int IsCelebrate = Animator.StringToHash("IsCelebrate");
    }
}
