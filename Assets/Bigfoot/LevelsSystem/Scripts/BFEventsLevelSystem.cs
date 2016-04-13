using UnityEngine;
using System.Collections;
using System;
using Bigfoot;

public static class BFEventsLevelSystem  {

    public static Action<LevelDTO> OnRewardCanBeCollected;

    public static void RewardCanBeCollected(LevelDTO dto)
    {
        if (OnRewardCanBeCollected != null)
            OnRewardCanBeCollected(dto);
    }

    public static Action<LevelDTO> OnRewardWasCollected;

    public static void RewardWasCollected(LevelDTO dto)
    {
        if (OnRewardWasCollected != null)
            OnRewardWasCollected(dto);
    }
}
