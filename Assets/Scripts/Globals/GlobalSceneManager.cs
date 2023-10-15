using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MPUIKIT;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Monster
{
	public static class SceneName
	{
		public static string GameplayScene  = "GameplayScene";
		public static string StartMenuScene = "StartMenuScene";
	}
    
	public class GlobalSceneManager : MonoBehaviour
	{
		public static GlobalSceneManager Instance { get; private set; }

		public MMF_Player OnTransitionAnimation;
		
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

			if (OnTransitionAnimation)
				OnTransitionAnimation.Initialization();
		}

		public void LoadScene(string _name)
		{
			StartCoroutine(LoadSceneAsync(_name));
		}

		public IEnumerator LoadSceneAsync(string _name)
		{
			OnTransitionAnimation.Direction = MMFeedbacks.Directions.TopToBottom;
			OnTransitionAnimation.PlayFeedbacks();

			yield return new WaitUntil(() => !OnTransitionAnimation.IsPlaying);
			
			AsyncOperation _loadSceneAsync = SceneManager.LoadSceneAsync(_name);

			while (!_loadSceneAsync.isDone)
				yield return null;
            
			yield return new WaitForSeconds(0.33f);

			OnTransitionAnimation.Direction = MMFeedbacks.Directions.BottomToTop;
			OnTransitionAnimation.PlayFeedbacks();
		}
	}
}
