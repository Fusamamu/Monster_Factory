using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Monster
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform BulletSpawnTarget;

        [SerializeField] private float ShootInterval;
        [SerializeField] private BulletPool BulletPool;

        private void Start()
        {
            Initialized();
        }

        public void Initialized()
        {
            BulletPool.Init();
            StartCoroutine(ShootBulletCoroutine());
        }

        private IEnumerator ShootBulletCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(ShootInterval);

                var _bullet = BulletPool.Pool?.Get();
                if (_bullet)
                {
                    _bullet
                        .SetSpawnPosition(BulletSpawnTarget.position)
                        .SetDirection(transform.forward)
                        .StartFire();
                }
            }
        }
    }
}
