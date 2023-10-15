using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MPUIKIT;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Monster
{
	public class GlobalAudioManager : MonoBehaviour
	{
		public static GlobalAudioManager Instance { get; private set; }

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(gameObject);
			}
			else
			{
				Instance = this;
				DontDestroyOnLoad(this);
			}
		}
	}
}
