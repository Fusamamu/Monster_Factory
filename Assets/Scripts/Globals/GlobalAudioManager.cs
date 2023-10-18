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

		public AudioSource BGMSource;
		public AudioClip BGM;

		public AudioSource EffectSource;
		public AudioClip FootStep;
		public AudioClip BloodSplat;
		public AudioClip Shooting;

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
			
			PlayBGM();
		}

		public void PlayBGM()
		{
			BGMSource.Play();
		}

		public void PlayBloodSplat()
		{
			EffectSource.PlayOneShot(BloodSplat);
		}

		public void PlayShooting()
		{
			EffectSource.PlayOneShot(Shooting);
		}

		private bool isPlayFootStep; 
		
		public void PlayFootStep()
		{
			if(isPlayFootStep)
				return;
			isPlayFootStep = true;
			EffectSource.clip = FootStep;
			EffectSource.loop = true;
			EffectSource.Play();
		}

		public void StopPlayEffect()
		{
			isPlayFootStep = false;
			EffectSource.loop = false;
			EffectSource.Stop();
		}
	}
}
