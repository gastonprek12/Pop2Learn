using UnityEngine;
using System.Collections;

namespace Bigfoot
{

    public static class BFEVentsPointsSystem
    {

        public static System.Action<int> OnLevelCompleted;

        public static void LevelCompleted(int stars)
        {
            if (OnLevelCompleted != null)
            {
                OnLevelCompleted(stars);
            }
        }
    }

}