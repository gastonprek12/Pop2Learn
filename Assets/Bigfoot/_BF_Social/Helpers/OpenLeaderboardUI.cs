using UnityEngine;
using System.Collections;

public class OpenLeaderboardUI : MonoBehaviour {

	void OnClick()
	{
		#if BF_SOCIAL
			if (Social.localUser.authenticated)		
				Social.ShowLeaderboardUI();
			else
				Social.localUser.Authenticate (result => {});
		#else
			Debug.LogError("Need to add BF_SOCIAL compiler flag for this to work");
		#endif
	}
}
