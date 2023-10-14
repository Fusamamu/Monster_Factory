using System.Collections;
using System.Collections.Generic;
using Monster;
using UnityEngine;

namespace Monster
{
	public class Service : MonoBehaviour, IInitializable
	{
		public bool IsInit { get; protected set; }
        
		public virtual void Init()
		{
		}
	}
}
