using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Bigfoot
{

    [RequireComponent(typeof(TimeSystemController))]
    public class EnergyBar : MonoBehaviour
    {

        public int BarId;

        public string TimeToRefil = "03:00";

        public Sprite FirstSpriteEmpty;

        public Sprite FirstSpriteFilled;

        public Sprite MiddleEmpty;

        public Sprite MiddleFilled;

        public UILabel TimeLabel;

        List<UI2DSprite> _barSprites;

        int _currentAmount;

        TimeSystemController _timeSystem;

        DateTime localTimeStarted;

        DateTime timeStartedInServer;

        TimeSpan timeLeft;

        TimeSpan timeToReplenish;

        TimeSpan timeUsedFromPreviousSession;

        void Start()
        {
            // Cache a reference to the TimeSystemController
            _timeSystem = GetComponent<TimeSystemController>();

            // Initialize the list of sprites
            _barSprites = new List<UI2DSprite>();

            // Add each of the childs of the transform to the barSprites list
            for (int i = 0; i < transform.childCount; i++)
                _barSprites.Add(transform.GetChild(i).GetComponent<UI2DSprite>());

            // After testing, get this from a pref
            _currentAmount = PlayerPrefs.GetInt("EnergyBar_" + BarId, _barSprites.Count);

            // Update the UI
            UpdateUI();

            // Get the time started in the server
            StartCoroutine(_timeSystem.GetCurrentTime(StartTimer));

            // Parse from a string, how long it takes to fill a new energy bar
            timeToReplenish = new TimeSpan(0, Int32.Parse(TimeToRefil.Substring(0, 2)), Int32.Parse(TimeToRefil.Substring(3, 2)));

            // If we had any remaining time, get it from a pref
            string time = PlayerPrefs.GetString("RemainingTime", "");
            if (time != "")
            {
                timeUsedFromPreviousSession = TimeSpan.Parse(time);
            }
            else
                timeUsedFromPreviousSession = new TimeSpan(0, 0, 0);
        }

        void StartTimer(DateTime time)
        {
            timeStartedInServer = time;
        }

        void Update()
        {
            if (_currentAmount < _barSprites.Count)
            {
                // We have to set the time where the counter started locally. It only sets to now when we just give the user a bar
                if (localTimeStarted == DateTime.MinValue)
                    localTimeStarted = DateTime.Now;

                // How much time is left equals: (How much time you have to wait for a life - How much time passed between local time started and now) - What you already waited in the last session
                timeLeft = timeToReplenish - (new TimeSpan(0, DateTime.Now.Minute, DateTime.Now.Second) - new TimeSpan(0, localTimeStarted.Minute, localTimeStarted.Second)) - timeUsedFromPreviousSession;

                //Fix for a fuckin weird bug
                bool hardReset = false;
                if (timeLeft.TotalSeconds > timeToReplenish.TotalSeconds)
                    hardReset = true;

                if (TimeLabel != null)
                {
                    string min = timeLeft.Minutes > 9 ? timeLeft.Minutes.ToString() : "0" + timeLeft.Minutes.ToString();
                    string sec = timeLeft.Seconds > 9 ? timeLeft.Seconds.ToString() : "0" + timeLeft.Seconds.ToString();
                    TimeLabel.text = string.Format("{0}:{1}", min, sec);
                }

                if (timeLeft.TotalSeconds <= 0 || hardReset)
                {
                    timeUsedFromPreviousSession = new TimeSpan(0, 0, 0);
                    PlayerPrefs.SetString("RemainingTime", "");

                    // Check if ok
                    StartCoroutine(_timeSystem.GetCurrentTime(CheckIfValidTime));

                    // We give the user the life. If its not valid, it gets taken away. We also reset the local time that the counter started
                    localTimeStarted = DateTime.MinValue;

                    _currentAmount++;
                    UpdateUI();


                    // Get Starting Value again
                    StartCoroutine(_timeSystem.GetCurrentTime(StartTimer));
                }
            }
            else
            {
                TimeLabel.text = "";
            }
        }

        void CheckIfValidTime(DateTime currentTimeInServer)
        {
            TimeSpan timeSpan = currentTimeInServer - timeStartedInServer;
            // We allow 1 minute delay to the server
            if (timeSpan.TotalSeconds - timeToReplenish.TotalSeconds + 60 >= 0)
            {
                Debug.Log("Score is valid");
            }
            else
            {
                _currentAmount--;
                UpdateUI();
                // TODO: Report as possible cheater
            }

            timeStartedInServer = currentTimeInServer;
        }

        void UpdateUI()
        {
            if (_currentAmount < 1)
                _barSprites[0].sprite2D = FirstSpriteEmpty;
            else
                _barSprites[0].sprite2D = FirstSpriteFilled;

            for (int i = 2; i < _barSprites.Count + 1; i++)
                if (_currentAmount < i)
                    _barSprites[i - 1].sprite2D = MiddleEmpty;
                else
                    _barSprites[i - 1].sprite2D = MiddleFilled;

            PlayerPrefs.SetInt("EnergyBar_" + BarId, _currentAmount);
        }

        void OnEnable()
        {
            BFEventsEnergySystem.OnEnergyWon += AddEnergy;
            BFEventsEnergySystem.OnEnergyLost += RemoveEnergy;
        }

        void OnDisable()
        {
            BFEventsEnergySystem.OnEnergyWon -= AddEnergy;
            BFEventsEnergySystem.OnEnergyLost -= RemoveEnergy;

            PlayerPrefs.SetString("RemainingTime", (timeToReplenish - timeLeft).ToString());
        }

        void AddEnergy(int amountEnergy, int barId)
        {
            if (BarId == barId)
            {
                _currentAmount++;

                if (_currentAmount > _barSprites.Count)
                    _currentAmount = _barSprites.Count;

                UpdateUI();
            }
        }

        void RemoveEnergy(int amountEnergy, int barId)
        {
            if (BarId == barId)
            {
                if (_currentAmount >= 1)
                    _currentAmount--;

                UpdateUI();
            }
        }
    }
}