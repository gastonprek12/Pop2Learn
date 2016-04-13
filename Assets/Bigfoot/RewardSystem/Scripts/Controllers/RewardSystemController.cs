using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Bigfoot
{
    [RequireComponent(typeof(TimeSystemController))]
    public class RewardSystemController : MonoBehaviour
    {
        /// <summary>
        /// The parent of the reward panel.
        /// </summary>
        public GameObject RewardPanelParent;

        /// <summary>
        /// The available rewards to give to the player each day. Here you can set the order in 
        /// which they are given to the Player.
        /// </summary>
        public BF_Item[] AvailableRewards;

        /// <summary>
        /// The reward grid. This will be used to fetch the RewardGridElements
        /// </summary>
        public UIGrid RewardGrid;

        [HideInInspector]
        /// <summary>
        /// The current day we are in the reward system;.
        /// </summary>
        public int CurrentDay;

        /// <summary>
        /// Cached reference to the TimeSystem
        /// </summary>
        TimeSystemController _timeSystem;

        /// <summary>
        /// All the RewardGridElements
        /// </summary>
        List<RewardGridElement> _rewardGridElements;

        void Start()
        {
            // Cache a reference for the TimeSystem
            _timeSystem = GetComponent<TimeSystemController>();

            // Load the CurrentDay from our player prefs, or start it as 0
            CurrentDay = PlayerPrefs.GetInt("RS_CurrentDay", 0);

            if (RewardGrid != null)
            {
                // Initialize the RewardGridElement list
                _rewardGridElements = new List<RewardGridElement>();

                // Populate the list with all the available RewardGridElements
                foreach (Transform t in RewardGrid.GetChildList())
                    _rewardGridElements.Add(t.GetComponent<RewardGridElement>());

                // Unlock all the previously unlocked Rewards
                UnlockPreviousRewards();

                if (_rewardGridElements.Count == AvailableRewards.Length)
                    // Check if it's valid to give a reward to the Player
                    StartCoroutine(_timeSystem.GetAmountOfDaysChangedSinceLastLogin(CheckIfShouldReward));
                else
                    Debug.Log("RewardSystem :: The lenght of the available rewards is not the same as the amount of RewardGridElements");
            }
            else
                Debug.Log("RewardSystem :: You are trying to use the Reward System, but the UIGrid for the Rewards is null");

        }

        void CheckIfShouldReward(int amountOfDaysSinceLastLogin)
        {
            // Only one day has passed, we give the Player the next reward
            if (amountOfDaysSinceLastLogin >= 1 && amountOfDaysSinceLastLogin < 2)
            {
                AdvanceOneDay();
            }
            // The player lost the reward because he didn't login in consecutive days
            else if (amountOfDaysSinceLastLogin >= 2)
            {
                Restart();
                AdvanceOneDay();
            }
        }


        void AdvanceOneDay()
        {
            // Activate the reward parent
            RewardPanelParent.SetActive(true);

            // The user already completed all the rewards, so we give restart the counter,
            // so he can get the first reward again
            if (CurrentDay >= AvailableRewards.Length)
                Restart();

            // We give the Player the reward
            GiveReward();

            // We increment the day & save it to the PlayerPref
            CurrentDay++;
            PlayerPrefs.SetInt("RS_CurrentDay", CurrentDay);

            // Set in the TimeSystem now as the last login date
            _timeSystem.SetLastLoginToCurrentTime();
        }

        void GiveReward()
        {
            BF_Item currentReward = AvailableRewards[CurrentDay];

            _rewardGridElements[CurrentDay].UnlockReward(true);

            BFEventsRewards.RewardGiven(currentReward);
        }

        void Restart()
        {
            CurrentDay = 0;
        }

        void UnlockPreviousRewards()
        {
            // Unlock all the previously unlocked rewards, before showing the particle
            // for the newly unlocked reward
            for (int i = 0; i < CurrentDay; i++)
                _rewardGridElements[i].UnlockReward(false);
        }

        public void CloseRewardPanel()
        {
            RewardPanelParent.SetActive(false);
        }

    }
}