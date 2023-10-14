using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Monster
{
    public class Bullet : MonoBehaviour, IEntity, IPoolAble<Bullet>
    {
        public bool IsInPool { get; set; }
        public IObjectPool<Bullet> Pool { get; private set; }
        
        [SerializeField] private Transform TargetTransform;

        [SerializeField] private float Speed;

        private Vector3 targetPos;
        private Vector3 direction;
        private bool isFiring;

        public void Initialized()
        {
            
        }

        public Bullet SetSpawnPosition(Vector3 _pos)
        {
            transform.position = _pos;
            return this;
        }

        private Bullet SetTarget(Vector3 _position)
        {
            targetPos = _position;
            return this;
        }

        public Bullet SetDirection(Vector3 _normalDir)
        {
            direction = _normalDir.normalized;
            return this;
        }

        public void StartFire()
        {
            isFiring = true;
        }

        private void Update()
        {
            if (isFiring)
                Fire();
        }

        public void Fire()
        {
            TargetTransform.position += direction * Speed * Time.deltaTime;
        }

        public void SetPool(IObjectPool<Bullet> _pool)
        {
            Pool = _pool;
        }

        public void ReturnToPool()
        {
            if(!IsInPool)
                Pool?.Release(this);
        }
    }
}
