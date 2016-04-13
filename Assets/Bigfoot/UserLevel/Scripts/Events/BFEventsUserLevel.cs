using System;

namespace Bigfoot
{
    public class BFEventsUserLevel
    {
        public static Action<int> OnXpWon;

        public static void XpWon(int amountXp)
        {
            if (OnXpWon != null)
                OnXpWon(amountXp);
        }

        public static Action<int> OnLevelUp;

        public static void LevelUp(int levelNumber)
        {
            if (OnLevelUp != null)
                OnLevelUp(levelNumber);
        }
    }
}