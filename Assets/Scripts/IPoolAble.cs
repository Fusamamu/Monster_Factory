using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Monster
{
	public interface IPoolAble<T> where T : Component, IEntity
	{
		public IObjectPool<T> Pool { get; }
        
		public bool IsInPool { get; set; }

		public void SetPool(IObjectPool<T> _pool);
		
		public void ReturnToPool();
	}
}
