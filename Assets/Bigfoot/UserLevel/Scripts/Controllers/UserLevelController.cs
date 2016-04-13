using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class UserLevelController : MonoBehaviour
    {

        public UserLevelTier[] LevelTiers;
        public UIProgressBar ProgessBar;
        public float AnimSpeed;

        [HideInInspector]
        public float CurrentXp;
        [HideInInspector]
        public int CurrentLevel;
        [HideInInspector]
        public float CurrentPercentage;
        [HideInInspector]
        public int PercentageAsInt;
        [HideInInspector]
        public Sprite LevelIcon;

        void OnEnable()
        {
            BFEventsUserLevel.OnXpWon += XpWon;
        }

        void OnDisable()
        {
            BFEventsUserLevel.OnXpWon -= XpWon;
        }

        void Start()
        {
            CurrentLevel = PlayerPrefs.GetInt("UL_CurrentLevel", 1);
            CurrentPercentage = PlayerPrefs.GetFloat("UL_CurrentPercentage", 0);
            PercentageAsInt = (int)(CurrentPercentage * 100);
            if (CurrentLevel <= LevelTiers.Length)
            {
                CurrentXp = LevelTiers[CurrentLevel - 1].LevelXp * CurrentPercentage;
                ProgessBar.value = CurrentPercentage;
                LevelIcon = LevelTiers[CurrentLevel - 1].LevelIcon;
            }
            else
            {
                CurrentXp = 0;
                ProgessBar.value = 1;
                LevelIcon = LevelTiers[LevelTiers.Length - 1].LevelIcon;
            }
        }

        void XpWon(int xpWon)
        {
            CurrentXp += xpWon;
            int levelsWon = 0;
            while (CurrentLevel <= LevelTiers.Length && CurrentXp >= LevelTiers[CurrentLevel - 1].LevelXp)
            {
                CurrentXp -= LevelTiers[CurrentLevel - 1].LevelXp;
                CurrentLevel++;
                BFEventsUserLevel.LevelUp(CurrentLevel);
                levelsWon++;
            }

            StartCoroutine(AnimateXpBar(levelsWon, CurrentXp));
            if (CurrentLevel <= LevelTiers.Length)
                LevelIcon = LevelTiers[CurrentLevel - 1].LevelIcon;
            else
                LevelIcon = LevelTiers[LevelTiers.Length - 1].LevelIcon;
        }

        IEnumerator AnimateXpBar(int levelsWon, float currentXp)
        {
            while (levelsWon > 0)
            {
                yield return StartCoroutine(AnimateBar(CurrentPercentage, 1));
                CurrentPercentage = 0;
                levelsWon--;
            }

            if (CurrentLevel < LevelTiers.Length)
            {
                float prevPctge = CurrentPercentage;
                CurrentPercentage = CurrentXp / LevelTiers[CurrentLevel - 1].LevelXp;
                PercentageAsInt = (int)(CurrentPercentage * 100);
                yield return StartCoroutine(AnimateBar(prevPctge, CurrentPercentage));
            }
            else
            {
                CurrentPercentage = 1;
                ProgessBar.value = 1;
                PercentageAsInt = 100;
            }
        }

        IEnumerator AnimateBar(float start, float end)
        {
            ProgessBar.value = start;
            while (ProgessBar.value < end)
            {
                ProgessBar.value += Time.deltaTime * AnimSpeed;
                yield return new WaitForEndOfFrame();
            }
        }

        void OnDestroy()
        {
            // Before destroying the UserLevelController, save the level and the current progress in a PlayerPref
            PlayerPrefs.SetInt("UL_CurrentLevel", CurrentLevel);
            PlayerPrefs.SetFloat("UL_CurrentPercentage", CurrentPercentage);
        }
    }

    [System.Serializable]
    public class UserLevelTier
    {
        public float LevelXp;
        public Sprite LevelIcon;
    }

}