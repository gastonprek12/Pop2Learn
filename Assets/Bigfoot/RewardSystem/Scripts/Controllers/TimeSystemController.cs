using UnityEngine;
using System.Collections;
using System;
using System.Globalization;

namespace Bigfoot
{
    public class TimeSystemController : MonoBehaviour
    {

        public bool UseLocal = true;

        public string WebserviceUrl = "http://currentmillis.com/api/millis-since-unix-epoch.php";

        DateTime _lastLogin;

        DateTime _currentTime;

        void Awake()
        {
            string dateString = PlayerPrefs.GetString("TS_LastLogin", "");
            if (!string.IsNullOrEmpty(dateString))
                _lastLogin = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture);
            else
                _lastLogin = DateTime.MinValue;
        }

        IEnumerator UpdateCurrentTime()
        {
            if (UseLocal)
            {
                _currentTime = DateTime.UtcNow;
                yield return new WaitForEndOfFrame();
            }
            else
                yield return StartCoroutine(GetTimeFromServer());
        }

        IEnumerator GetTimeFromServer()
        {
            WWW www = new WWW(WebserviceUrl);

            yield return www;

            if (!string.IsNullOrEmpty(www.error))
                Debug.Log("You tried to fetch the time for an URL which has the following error: " + www.error);
            else
                _currentTime = ParseDateTimeFromWWW(www.text);

        }

        DateTime ParseDateTimeFromWWW(string milisecs)
        {
            return new DateTime(long.Parse(milisecs));
        }

        public void SetLastLoginToCurrentTime()
        {
            _lastLogin = _currentTime;
            PlayerPrefs.SetString("TS_LastLogin", _lastLogin.ToString("yyyyMMdd"));
        }

        public TimeSpan GetTimespanBetweenLastLoginAndNow()
        {
            UpdateCurrentTime();
            return _currentTime - _lastLogin;
        }

        public IEnumerator GetAmountOfDaysChangedSinceLastLogin(System.Action<int> callback)
        {
            yield return StartCoroutine(UpdateCurrentTime());
            int difference = _currentTime.DayOfYear - _lastLogin.DayOfYear;
            if (difference >= 0)
                callback(difference);
            else if (difference == -364)
                callback(1);
            else
                callback(2);
        }

        public IEnumerator GetCurrentTime(System.Action<DateTime> callback)
        {
            yield return StartCoroutine(UpdateCurrentTime());
            callback(_currentTime);
        }

    }

}