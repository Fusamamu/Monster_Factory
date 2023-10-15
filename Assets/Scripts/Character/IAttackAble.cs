using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public interface IAttackAble : IVisible
    {
        public Transform ShootTarget { get; }
        public bool IsTargetLock { get; set; }
    }
}
