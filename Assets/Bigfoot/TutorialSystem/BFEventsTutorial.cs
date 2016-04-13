using UnityEngine;
using System.Collections;
using System;

namespace Bigfoot
{
    public static class BFEventsTutorial
    {

        public static Action<int, GameObject, string> OnShowTutorial;

        public static void ShowTutorial(int stage, GameObject tutorialPrefab, string description)
        {
            if (OnShowTutorial != null)
            {
                OnShowTutorial(stage, tutorialPrefab, description);
            }
        }

        public static Action<int> OnStageCompleted;

        public static void StageCompleted(int stage)
        {
            if (OnStageCompleted != null)
                OnStageCompleted(stage);
        }

        public static Action OnStageBecameAvailable;

        public static void StageBecameAvailable()
        {
            if (OnStageBecameAvailable != null)
                OnStageBecameAvailable();
        }

    }
}