using UnityEngine;
using System.Collections;
#if BF_SOCIAL
using GooglePlayGames;
#endif

public class AuthenticateSocialPlatform : MonoBehaviour {

	public bool AuthenticateOnStart = true;

	public bool AuthenticateOnClick = false;

	void Start () 
	{
		//PlayerPrefs.DeleteAll();
		if (AuthenticateOnStart == true)
			Authenticate ();
	}	

	void OnClick () 
	{
		if (AuthenticateOnClick == true)
			Authenticate ();
	}	

	void Authenticate()
	{
		#if BF_SOCIAL
			#if UNITY_ANDROID
				PlayGamesPlatform.Activate();
			#endif
			
			Social.localUser.Authenticate (result => {});
		#else
			Debug.LogError("Need to add BF_SOCIAL compiler flag for this to work");
		#endif
	}

}
