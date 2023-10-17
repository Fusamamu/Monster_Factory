using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class BulletPool : PoolSystem<Bullet>
    {
        protected override Bullet CreateObject()
        {
            var _bullet = Instantiate(Prefab);//, populationParent.transform);

            _bullet.name = $"_bullet_";
            _bullet.SetPool(Pool);
            _bullet.Initialized();

            return _bullet;
        }

        protected override void OnGetObject(Bullet _bullet)
        {
            _bullet.gameObject.SetActive(true);
            _bullet.IsInPool = false;
            
            ActivePoolObjects.Add(_bullet);
        }

        protected override void OnRelease(Bullet _bullet)
        {
            _bullet.IsInPool = true;
            _bullet.gameObject.SetActive(false);
        }

        protected override void OnObjectDestroyed(Bullet _bullet)
        {
            Destroy(_bullet.gameObject);
        }
        
        public override void ClearPool()
        {
            if (ActivePoolObjects.Count > 0)
            {
                ActivePoolObjects.ForEach(_person => _person.ReturnToPool());
                ActivePoolObjects.Clear();
            }
        }
    }
}
