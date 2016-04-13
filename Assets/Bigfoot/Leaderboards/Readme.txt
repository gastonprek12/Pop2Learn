==================================================================
							HOW TO USE [1hs]
==================================================================

* Import the latest Bigfoot Asset .unitypackage in your project, and select to include Leaderboards.
* Put the 'GlobalLeaderboard' prefab in your scene.
* Modify the url from where to get the JSON with the information (Should have at least 'Name' and 'Score')
* You can modify the UI for the prefab 'PlayerScore', to match your look n feel
* If you wish to add more fields other than Name and Score, you have to do the following:
	* Add the field to the JSON result
	* Add a public variable in the script PlayerScore with the name of the field
	* Add a UIElement in the 'PlayerScore' prefab, and bind it with a 'PropertyBinding' element to the variable just created in the UI