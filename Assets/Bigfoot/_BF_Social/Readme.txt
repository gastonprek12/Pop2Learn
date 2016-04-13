
===================================================
HOW TO USE
===================================================

1. Put the prefab 'SocialController' on the first scene of the game.
2. Open up 'BFConstantsSocial' and fill in the keys & values for the achievements & leaderboards.
 (!!! Keep in mind, the keys must be the same on iOS & Android. So first create them on Android, and then copy the keys to iOS)
3. Wherever you want the user to authenticate, drop the 'AuthenticateSocial" prefab in the scene, and select if you want it to authenticate on Start or on Click
4. Just send the events in your code whenver you want to give an achievement or a score
		BFEventsSocial.GiveAchievement(AchievementsKeyEnum)
		BFEventsSocial.ReportScore(long,LeaderboardsKeyEnum)

