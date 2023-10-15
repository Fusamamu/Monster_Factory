using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public interface IDamageable
    {
        public int HP { get; }

        public void RecieveDamage(int _damge) { }
    }
}
