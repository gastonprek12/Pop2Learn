using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class TutorialGlobalStage : MonoBehaviour
    {

        public int stage;
        public GameObject NextLabel;
        bool canSkip;

        void OnEnable()
        {

        }

        public void EnableCollider()
        {
            canSkip = true;
            NextLabel.SetActive(true);
        }

        void OnClick()
        {
            if (canSkip)
            {
                NGUITools.Destroy(gameObject);
                BFEventsTutorial.StageCompleted(stage);
            }
        }
    }
}