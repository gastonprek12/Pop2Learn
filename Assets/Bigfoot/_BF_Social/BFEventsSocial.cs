using System;

namespace Bigfoot
{
	public static class BFEventsSocial 
	{
		/// <summary>
		/// Give the achievement to the user.
		/// </summary>
		public static Action<BFKSocialAchievementKeys> OnGiveAchievement;
		public static void GiveAchievement(BFKSocialAchievementKeys achiev)
		{
			if(OnGiveAchievement != null)
				OnGiveAchievement(achiev);
		}

		/// <summary>
		/// Report a score.
		/// </summary>
		public static Action<long, BFKSocialLeaderboardKeys> OnReportScore;
		public static void ReportScore(long score, BFKSocialLeaderboardKeys leaderboard)
		{
			if(OnReportScore != null)
				OnReportScore(score, leaderboard);
		}
	}
}
