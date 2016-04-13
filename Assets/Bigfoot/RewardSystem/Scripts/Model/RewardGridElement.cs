using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class RewardGridElement : MonoBehaviour
    {

        /// <summary>
        /// The sprite to show when the reward is still locked
        /// </summary>
        public Sprite LockedRewardSprite;

        /// <summary>
        /// The sprite to show when the reward is already unlocked
        /// </summary>
        public Sprite UnlockedRewardSprite;

        /// <summary>
        /// The background sprite of the reward. This will be locked/unlocked
        /// </summary>
        public UI2DSprite BackgroundSprite;

        /// <summary>
        /// The particle effect to show when the reward is first unlocked
        /// </summary>
        public GameObject ParticleEffect;

        /// <summary>
        /// Mehod called by the RewardSystemController to unlock a Reward
        /// </summary>
        /// <param name="showParticle">If set to <c>true</c> show particle.</param>
        public void UnlockReward(bool showParticle)
        {
            BackgroundSprite.sprite2D = UnlockedRewardSprite;

            if (showParticle)
                ParticleEffect.SetActive(true);
        }
    }

}