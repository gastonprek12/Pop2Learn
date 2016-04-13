using UnityEngine;
using System.Collections;
using System;

namespace Bigfoot
{

    public static class BFEventsGameFlow
    {

        public static Action OnTryAgain;
        public static Action OnGoToMenu;
        public static Action OnGameWon;
        public static Action OnGameLost;
        public static Action OnPause;
        public static Action OnUnpause;
        public static Action OnGameEnded;
		public static Action OnShowSpinner;
		public static Action OnHideSpinner;
        public static Action<BFKPanelName> OnShowPanel;
        public static Action OnBack;

        public static void TryAgain()
        {
            if (OnTryAgain != null)
                OnTryAgain();
        }

        public static void GoToMenu()
        {
            if (OnGoToMenu != null)
                OnGoToMenu();
        }

        public static void GameWon()
        {
            if (OnGameEnded != null)
                OnGameEnded();
            if (OnGameWon != null)
                OnGameWon();
        }

        public static void GameLost()
        {
            if (OnGameEnded != null)
                OnGameEnded();
            if (OnGameLost != null)
                OnGameLost();
        }

        public static void Pause()
        {
            if (OnPause != null)
                OnPause();
        }

        public static void Unpause()
        {
            if (OnUnpause != null)
                OnUnpause();
        }

		public static void ShowSpinner()
		{
			if (OnShowSpinner != null)
				OnShowSpinner();
		}

		public static void HideSpinner()
		{
			if (OnHideSpinner != null)
				OnHideSpinner();
		}

        public static void ShowPanel(BFKPanelName name)
        {
            if(OnShowPanel != null)
                OnShowPanel(name);
        }

        public static void Back()
        {
            if (OnBack != null)
                OnBack();
        }
    }
}