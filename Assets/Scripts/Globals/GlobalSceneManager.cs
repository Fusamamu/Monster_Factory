using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Feedbacks;
using MPUIKIT;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Monster
{
	public static class SceneName
	{
		public static string GameplayScene  = "PrototypeScene";
		public static string StartMenuScene = "StartScene";
	}
    
	public class GlobalSceneManager : MonoBehaviour
	{
		public static GlobalSceneManager Instance { get; private set; }

		public MMF_Player OnTransitionAnimation;

		public GameObject Disclaimer;

		public GameObject Document;
		public List<GameObject> Contents = new List<GameObject>();
		public int ContentIndex;

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
		
		public void LoadNewGameScene(string _name)
		{
			StartCoroutine(LoadNewGameAsync(_name));
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
		
		public IEnumerator LoadNewGameAsync(string _name)
		{
			OnTransitionAnimation.Direction = MMFeedbacks.Directions.TopToBottom;
			OnTransitionAnimation.PlayFeedbacks();

			yield return new WaitUntil(() => !OnTransitionAnimation.IsPlaying);
			
			Disclaimer.SetActive(true);
			
			yield return new WaitForSeconds(3f);
			
			AsyncOperation _loadSceneAsync = SceneManager.LoadSceneAsync(_name);

			while (!_loadSceneAsync.isDone)
				yield return null;
            
			yield return new WaitForSeconds(0.33f);
			
			Disclaimer.SetActive(false);
			
			yield return new WaitForSeconds(1f);
			
			Document.SetActive(true);

			while (ContentIndex < Contents.Count - 1)
			{
				foreach (var _content in Contents)
					_content.SetActive(false);

				Contents[ContentIndex].SetActive(true);
				ContentIndex++;
				
				yield return new WaitForSeconds(3f);
			}

			ContentIndex = 0;
			foreach (var _content in Contents)
				_content.SetActive(false);
			
			Document.SetActive(false);

			
			OnTransitionAnimation.Direction = MMFeedbacks.Directions.BottomToTop;
			OnTransitionAnimation.PlayFeedbacks();
		}
	}
}
