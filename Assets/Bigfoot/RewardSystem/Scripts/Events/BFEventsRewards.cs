using System;

namespace Bigfoot
{
    public class BFEventsRewards
    {
		public static Action<BF_Item> OnRewardGiven;

        public static void RewardGiven(BF_Item reward)
        {
            if (OnRewardGiven != null)
				OnRewardGiven(reward);
        }
    }
}