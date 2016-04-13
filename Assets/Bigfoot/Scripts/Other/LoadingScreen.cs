using UnityEngine;
using System.Collections;


/// <summary>
/// Show a loading screen after the level started loading
/// </summary>
namespace Bigfoot
{
	public class LoadingScreen : MonoBehaviour {

		public GameObject Image;

		void OnEnable () {
			GeneralEventSystem.OnLevelStartLoadingEvent += ShowLoading;
		}

		void OnDisable () {
			GeneralEventSystem.OnLevelStartLoadingEvent -= ShowLoading;
		}

		void ShowLoading(string s)
		{
			Image.SetActive (true);
		}
	}
}