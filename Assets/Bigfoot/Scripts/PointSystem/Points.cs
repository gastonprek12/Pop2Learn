using UnityEngine;
using System.Collections;
using System;

namespace Bigfoot
{
    [Serializable]
    public class PointsDTO : MonoBehaviour
    {

        public int minScore;

        public int mediumScore;

        public int maxScore;

        [HideInInspector]
        private int userScore;

        public int GetUserScore()
        {
            return userScore;
        }

        public void SetUserScore(int score)
        {
            userScore = score;
            int stars = 0;
            if (userScore > maxScore)
                stars = 3;
            else if (userScore > mediumScore)
                stars = 2;
            else if (userScore > minScore)
                stars = 1;
            BFEVentsPointsSystem.LevelCompleted(stars);
        }
    }
}