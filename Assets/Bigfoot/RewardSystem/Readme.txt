==================================================================
							HOW TO USE [6hs]
==================================================================

* Import the latest Bigfoot Asset .unitypackage in your project, and select to include RewardSystem.
* Open up the script 'Reward Enum', and fill in all the keys you would like to have for your rewards. You need one key for each reward.
* Put the RewardSystem prefab in your scene.
* In the RewardSystem object, set the amount of Avaiable rewards, by putting in their name(optional) and their respective key (previously set).
* Under RewardGrid, you have all the graphic elements of the RewardGrid. You can here set the icon, particle effect, and more details for each Reward.
* Make sure that you have a reference to the RewardGrid in the RewardSystem, and the amount of children of the grid is the same as the amount of available rewards.
* You can set the RewardSystem to use local time, or a remote webservice. If you want to change the URL that queries for the time, you might need to re-implemente the 'ParseDateTimeFromWWW' method
* You can now subscribe to RewardEvents.OnRewardGiven, and receive the events as to which 'RewardEnum' was unlocked, and act based on it