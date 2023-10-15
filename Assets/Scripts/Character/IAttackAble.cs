using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public interface IAttackAble 
    { 
        public Transform AttackTarget { get; }
        public bool IsTargetLock { get; set; }

        public void Attack(IDamageable _attackTarget) { }
    }
}
