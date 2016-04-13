using UnityEngine;
using System.Collections;

namespace Bigfoot
{
	public class SocialController : MonoBehaviour 
	{
		void OnEnable()
		{
			BFEventsSocial.OnGiveAchievement += GiveAchievement;
			BFEventsSocial.OnReportScore += ReportScore;
		}

		void OnDisable()
		{
			BFEventsSocial.OnGiveAchievement -= GiveAchievement;
			BFEventsSocial.OnReportScore -= ReportScore;
		}

		void GiveAchievement(BFKSocialAchievementKeys achiev)
		{
			#if BF_SOCIAL
				// Only do this if the user is authenticated
				if (Social.localUser.authenticated) 
				{
					// Get the value of the achiev
					int achievIndex = (int) achiev;
					string achievValue = ((BFKSocialAchievementValues) achievIndex).ToString();

					string nameAchiev = "Achievement_" + achievValue;

					// See if we gave the user that achievement already
					if (PlayerPrefs.GetInt (nameAchiev, 0) == 0) 
					{
						Social.ReportProgress (achievValue, 100, callback => {});
						PlayerPrefs.SetInt (nameAchiev, 1);
					}
				}			
			#else
				Debug.LogError("Need to add BF_SOCIAL compiler flag for this to work");
			#endif
		}

		void ReportScore(long score, BFKSocialLeaderboardKeys leaderboard)
		{
			#if BF_SOCIAL
				// Only do this if the user is authenticated
				if (Social.localUser.authenticated) 
				{
					// Get the value of the achiev
					int leaderboardndex = (int) leaderboard;
					string leaderboardValue = ((BFKSocialLeaderboardValues) leaderboardndex).ToString();

					Social.ReportScore(score, leaderboardValue, callback => {});
				}
			#else
				Debug.LogError("Need to add BF_SOCIAL compiler flag for this to work");
			#endif
		}
	}
}
