using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class UnlockOnLevel : MonoBehaviour
    {

        public int LevelToUnlock;

        public Sprite UnlockedSprite;

        public UI2DSprite SpriteRend;

        void OnEnable()
        {
            BFEventsUserLevel.OnLevelUp += LevelUp;
        }

        void OnDisable()
        {
            BFEventsUserLevel.OnLevelUp -= LevelUp;
        }

        void LevelUp(int level)
        {
            if (level == LevelToUnlock)
                UnlockReward();
        }

        void UnlockReward()
        {
            SpriteRend.sprite2D = UnlockedSprite;
        }
    }
}