using UnityEngine;
using System.Collections;

/***
 * 
 * NGUI DEFINE KEY: NGUI
 * SOOMLA DEFINE KEY: SOOMLA
 * FACEBOOK DEFINE KEY: FACEBOOK
 * PRIME31 DEFINE KEY: PRIME31
 * ACHIEVEMENTS AND SOCIAL: BF_SOCIAL
 * GAME ANALYTICS : GAME_ANALYTICS
 * 
 * */

public enum BFKItemKeys
{
    SOFT,
    HARD,
    ATTACK,
	DEFENSE,
	GENERAL,
	BALLPATH,
	REPOSITION,
	DOUBLEMOVE,
	DOUBLETIME,
	REDUCEGOAL
}

public enum BFKCurrencyKeys
{
    MONEY,
    SOFT,
    HARD
}

public static class BFKPreferences
{
	public static string PREF_MUSIC_ON = "PREF_MUSIC_ON";
	public static string PREF_SOUND_ON = "PREF_SOUND_ON";
	public static string PREF_JOYSTICK_ON = "PREF_JOYSTICK_ON";
	public static string PREF_SCREENSHAKE_ON = "PREF_SCREENSHAKE_ON";
	public static float MAX_VOLUME_MUSIC = 0.3f;
	public static float MAX_VOLUME_SFX = 1.0f;
	public static string TIME_TO_WAIT_FOR_LIFE = "01:00";
	public static int MAX_AMOUNT_OF_LIVES = 5;
	public static int SURVIVAL_STARTING_LIVES = 3;
}    

public static class BFKConfig
{
	public static string CompanyName = "bigfoot";
	public static string GameName = "poolball";
    public static string ServerIp = "23.102.132.46";//Local ip 192.168.0.12	
    public static int connectionPort = 25001;
	public static string FacebookIAPUrl = "http://bigfootgaming.net/Amy/Facebook/IAP/"; // URL where the IAP htmls will be
	public static string FacebookAchievUrl = "http://bigfootgaming.net/Amy/Facebook/opengraph/"; // URL where the achievments htmls will be
    public static int StoreVersionNumber = 6;
#if !UNITY_WEBPLAYER
    public static string USER_INFO_PATH = Application.persistentDataPath + "/userinfo.dat";
#else
    public static string USER_INFO_PATH = "user_info";
#endif

	public static string GetBundlePrefix()
	{
		return string.Format("com.{0}.{1}", BFKConfig.CompanyName, BFKConfig.GameName);
	}
}