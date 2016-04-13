using UnityEngine;
using System.Collections;

namespace Bigfoot
{
	public class SplashScreenController : MonoBehaviour {
		
		/// <summary>
		/// The percentage to bind to a label.
		/// </summary>
		[HideInInspector]
		public string Percentage;
		
		/// <summary>
		/// The target framerate.
		/// </summary>
		public int TargetFramerate = 60;
		
		/// <summary>
		/// Does it needs syncing?
		/// </summary>
		public bool NeedsSync = false;
		
		/// <summary>
		/// The name of the scene to load after.
		/// </summary>
		public string SceneName = "Menu";
		
		/// <summary>
		/// Objects to scroll through
		/// </summary>
		public GameObject[] ObjectsToShow;
		
		/// <summary>
		/// How long each object will show.
		/// </summary>
		public float TimeEachObject;
		
		bool _haveConnection = false;
		
		bool _synced = false;
		
		float _currentLoaded;
		
		void Start()
		{
			// Target Framerate for iOS if above 30
			Application.targetFrameRate = TargetFramerate;
			
			Percentage = "0%";
			
			if (NeedsSync)
				StartCoroutine (SyncPlayer ());
			else
				StartCoroutine (StartGame ());
		}
		
		IEnumerator SyncPlayer()
		{
			//Call API For sync
			if (_haveConnection && !_synced) 
			{
				_synced = true;
				
				yield return StartCoroutine(SimulateLoading(10f, 1f));
				
				// Sync the player with yield return StartCoroutine....
				
				Play ();
			}
		}
		
		IEnumerator StartGame()
		{
			foreach(GameObject go in ObjectsToShow)
			{
				go.SetActive(true);
				yield return new WaitForSeconds(TimeEachObject);
				go.SetActive(false);
			}
			
			Play ();
		}
		
		/// <summary>
		/// Simulates loading. Can be used in between server calls
		/// </summary>
		/// <returns></returns>
		/// <param name="upTo">Up to what percentage we want to go</param>
		/// <param name="timeLimit">How long it should take to get there</param>
		IEnumerator SimulateLoading(float upTo, float timeLimit)
		{
			float currentTime = 0;
			while (_currentLoaded != upTo) 
			{
				_currentLoaded = Mathf.Lerp(_currentLoaded, upTo, currentTime / timeLimit);
				Percentage = _currentLoaded.ToString("0") + "%";
				currentTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}
		
		public void Play()
		{
			GeneralEventSystem.Instance.LoadLevel(SceneName);
		}
	}
}
