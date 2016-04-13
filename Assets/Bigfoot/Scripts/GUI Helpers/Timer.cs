using UnityEngine;
using System.Collections;
using System;

namespace Bigfoot
{
    public class Timer : MonoBehaviour
    {

        public UILabel TimeLabel;
        DateTime startedTime;
        TimeSpan duration;
        bool isRunning;
        Action timerDelegate;
		TimeSpan timeLeft;
		float pausedSeconds;
		public bool showMinutes;

		public void StartTimer(TimeSpan timeDuration, Action tdelegate, DateTime start)
        {
            timerDelegate = tdelegate;
            duration = timeDuration;
			startedTime = start;
            isRunning = true;
        }



        // Update is called once per frame
        void Update()
        {
            if (isRunning)
            {
                timeLeft = duration - (DateTime.Now - startedTime);
                if (TimeLabel != null)
                {
                    string min = timeLeft.Minutes > 9 ? timeLeft.Minutes.ToString() : "0" + timeLeft.Minutes.ToString();
					string sec = timeLeft.Seconds > 9 ? timeLeft.Seconds.ToString() : "0" + timeLeft.Seconds.ToString();
					string hour = timeLeft.Hours > 9 ? timeLeft.Hours.ToString() : "0" +timeLeft.Hours.ToString();
					if (showMinutes)
						TimeLabel.text = string.Format("{0}:{1}", min, sec);
					else
						TimeLabel.text = string.Format("{0}", sec);
                }

                if (timeLeft.TotalSeconds <= 0)
                {
                    isRunning = false;
                    if (timerDelegate != null)
                        timerDelegate();
                }
            }
			else {
				pausedSeconds += Time.deltaTime;
			}
        }


	}
}