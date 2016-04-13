using UnityEngine;
using System.Collections;
using Bigfoot;
using System.Collections.Generic;
using System.Linq;

public class UserController : Singleton<UserController> {

	public LocalUser User;

	public bool UseServer = false;

	void OnEnable()
	{
		BFEventsUser.OnItemAmountChanged += ItemBalanceChanged;
	}

	void OnDisable()
	{
		BFEventsUser.OnItemAmountChanged -= ItemBalanceChanged;
	}

	void Start()
	{
		//if(UseServer)
			//APIController.Instance.RegisterAnon (RegistrationCallback);
		//else
			LoadUserInformation();
	}

	void RegistrationCallback(string newUserInfo)
	{
		// Get the userinfo from the server
		var userInfo = (Dictionary<string,object>) MiniJSON.Json.Deserialize (newUserInfo);
		
		// Create a new local user
		LocalUser localUser = new LocalUser ();
		
		// Initialize the User
		UpdateUserInfo (localUser, userInfo);
	}

	void UpdateUserInfo(LocalUser localUser, Dictionary<string,object> userInfo)
	{
		// Set all the information
		localUser.ID = System.Int32.Parse (userInfo["ID"].ToString());
		localUser.Name = userInfo ["name"].ToString ();
		localUser.Level = System.Int32.Parse (userInfo["Level"].ToString());
		localUser.Xp = System.Int32.Parse (userInfo["Xp"].ToString());
		if(userInfo ["FacebookID"] != null)
			localUser.FacebookID = userInfo ["FacebookID"].ToString ();
		
		// Cache the reference
		User = localUser;
		
		// Save the information
		SaveUserInformation ();
	}

	public void SaveUserInformation()
	{
		if(UseServer)
			PlayerPrefs.SetInt ("UserId", User.ID);
		else
			Serializer.SaveSerializableObjectToFile<LocalUser>(User, Application.persistentDataPath + "/localUser");
	}

	void LoadUserInformation()
	{
		User = Serializer.LoadSerializableObjectFromFile<LocalUser>(Application.persistentDataPath + "/localUser");
	}

	public void ItemBalanceChanged(BFKItemKeys key,  int amountChanged)
	{
		BF_Item itemToChange = User.Items.Where (it => it.ItemKey == key).FirstOrDefault ();

		itemToChange.ItemAmount += amountChanged;

		//if(UseServer)
		//	APIController.Instance.ItemBalanceChanged (User.ID,item.ItemKey, amountChanged);
		//else
		SaveUserInformation ();
	}

	/*


	void OnEnable()
	{
		#if BF_FACEBOOK
			BF_FacebookEvents.OnFacebookLogin += RegisterFacebook;
		#endif
	}

	void OnDisable()
	{
		#if BF_FACEBOOK
			BF_FacebookEvents.OnFacebookLogin -= RegisterFacebook;
		#endif
	}

	public void RegisterAnon()
	{
		//APIController.Instance.RegisterAnon (RegistrationCallback);
	}

	public void RegisterFacebook()
	{
		#if BF_FACEBOOK
			FB.API("/me?fields=first_name", Facebook.HttpMethod.GET, NameCallback);
		#endif
	}

	#if BF_FACEBOOK
	void NameCallback(FBResult result)
	{
		string username = "";

		if (!FB.IsLoggedIn)
			Debug.Log("Login cancelled by Player");
		else
		{
			IDictionary dict = Facebook.MiniJSON.Json.Deserialize(result.Text) as IDictionary;
			username = dict["first_name"].ToString();
		}

		// If I'm the login screen, it means I should register with Facebook in the Server
		if(_isInLoadingScreen)
			APIController.Instance.RegisterFacebook (FB.UserId, username, RegistrationCallback);
		// If I'm in another scene, its just a Sync
		else
		{
			APIController.Instance.SyncWithFacebook(User.ID, FB.UserId, username, SyncCallback);
		}
	}
	#endif



	void UpdateUserInfo(LocalUser localUser, Dictionary<string,object> userInfo)
	{
		// Set all the information
		localUser.ID = System.Int32.Parse (userInfo["ID"].ToString());
		localUser.Name = userInfo ["name"].ToString ();
		localUser.Level = System.Int32.Parse (userInfo["Level"].ToString());
		localUser.Xp = System.Int32.Parse (userInfo["Xp"].ToString());
		localUser.Gems = System.Int32.Parse (userInfo["Gems"].ToString());
		localUser.Coins = System.Int32.Parse (userInfo["Coins"].ToString());
		if(userInfo ["Country"] != null)
			localUser.Country = userInfo ["Country"].ToString ();
		localUser.WonChallenges = System.Int32.Parse (userInfo["WonChallenges"].ToString());
		localUser.LostChallenges = System.Int32.Parse (userInfo["LostChallenges"].ToString());
		localUser.CorrectAnswers = System.Int32.Parse (userInfo["CorrectAnswers"].ToString());
		localUser.WrongAnswers = System.Int32.Parse (userInfo["WrongAnswers"].ToString());
		if(userInfo ["FacebookID"] != null)
			localUser.FacebookID = userInfo ["FacebookID"].ToString ();

		// Cache the reference
		User = localUser;
		
		// Save the information
		SaveUserInformation ();

		GeneralEvents.HideSpinner ();

		UserEvents.UserFinishedLoading ();
	}


	public IEnumerator SyncInformation()
	{
		int userId = PlayerPrefs.GetInt ("UserId", 0);

		if (userId != 0)
			yield return StartCoroutine (APIController.Instance.GetPlayerInfoWithProgress (userId, SyncCallback));
		else
			yield return new WaitForEndOfFrame ();
	}

	void SyncCallback(string result)
	{
		// Get the userinfo from the server
		var userInfo = (Dictionary<string,object>) MiniJSON.Json.Deserialize (result);

		UpdateUserInfo (User, userInfo);

		if(userInfo ["LevelInfoes"] != null)
			MapUserProgress (userInfo ["LevelInfoes"]);
	}

	void MapUserProgress(object progress)
	{
		var userProgress = (List<object>) progress;

		LevelProgress levelProgress;

		if(userProgress != null)
		{
			foreach (object obj in userProgress) 
			{
				levelProgress = new LevelProgress();

				var levelInfo = (Dictionary<string,object>) obj;

				levelProgress.ChapterNumber = System.Int32.Parse (levelInfo["Chapter"].ToString());
				levelProgress.LevelNumber = System.Int32.Parse (levelInfo["Level"].ToString());
				levelProgress.Progress = System.Int32.Parse (levelInfo["Percentage"].ToString());

				Progress.Add(levelProgress);
			}
		}
	}

	public void CoinBalanceChanged(int amountChanged)
	{
		User.Coins += amountChanged;

		APIController.Instance.CoinBalanceChanged (User.ID, amountChanged);
	}

	public void GemBalanceChanged(int amountChanged)
	{
		User.Gems += amountChanged;
		
		APIController.Instance.GemBalanceChanged (User.ID, amountChanged);
	}
	*/
}
