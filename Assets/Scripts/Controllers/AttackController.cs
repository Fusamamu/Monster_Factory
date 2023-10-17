using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Monster
{
    public class AttackController : MonoBehaviour
    {
        public float AttackRange { get => attackRange; private set => attackRange = value; }

        public float AttackDelay { get => attackDelay; private set => attackDelay = value; }

        [Header("Parameter")]
        [SerializeField] private bool isAttack;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackDelay;
        [SerializeField] private int damage;

        public void AttackTarget(IAttackAble _attacker, IDamageable _target)
        {
            if (isAttack)
                return;
            StartCoroutine(DoAttack(_attacker, _target));
        }

        IEnumerator DoAttack(IAttackAble _attacker, IDamageable _target)
        {
            isAttack = true;

            _attacker.Attack(_target, damage);

            yield return new WaitForSeconds(attackDelay);
            isAttack = false;
        }

    }
}
