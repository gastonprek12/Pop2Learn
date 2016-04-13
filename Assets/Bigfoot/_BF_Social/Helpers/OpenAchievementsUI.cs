using UnityEngine;
using System.Collections;

public class OpenAchievementsUI : MonoBehaviour {

	void OnClick()
	{
		#if BF_SOCIAL
			if (Social.localUser.authenticated)		
				Social.ShowAchievementsUI();
			else
				Social.localUser.Authenticate (result => {});
		#else
			Debug.LogError("Need to add BF_SOCIAL compiler flag for this to work");
		#endif

	}
}
