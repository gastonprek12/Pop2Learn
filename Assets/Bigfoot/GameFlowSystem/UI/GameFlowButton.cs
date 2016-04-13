using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public enum GameFlowUse
    {
        PlayAgain,
        GoToMenu,
        GameWon,
        GameLost,
        Pause,
        Unpause
    }

    public class GameFlowButton : MonoBehaviour
    {

        public GameFlowUse UseAs;

        void OnMouseDown()
        {
            Action();
        }

        void OnClick()
        {
            Action();
        }

        void Action()
        {
            switch (UseAs)
            {
                case GameFlowUse.PlayAgain:
                    BFEventsGameFlow.TryAgain();
                    break;
                case GameFlowUse.GoToMenu:
                    BFEventsGameFlow.GoToMenu();
                    break;
                case GameFlowUse.GameWon:
                    BFEventsGameFlow.GameWon();
                    break;
                case GameFlowUse.GameLost:
                    BFEventsGameFlow.GameLost();
                    break;
                case GameFlowUse.Pause:
                    Time.timeScale = 0;
                    BFEventsGameFlow.Pause();
                    break;
                case GameFlowUse.Unpause:
                    Time.timeScale = 1;
                    BFEventsGameFlow.Unpause();
                    break;

            }
        }
    }
}