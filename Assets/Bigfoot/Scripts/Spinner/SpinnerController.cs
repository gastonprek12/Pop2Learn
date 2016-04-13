using UnityEngine;
using System.Collections;

namespace Bigfoot
{
	public class SpinnerController : MonoBehaviour {

		public GameObject Container;

		void OnEnable()
		{
			//GeneralEvents.OnHideSpinner += HideSpinner;
			//GeneralEvents.OnShowSpinner += ShowSpinner;
		}

		void OnDisable()
		{
			//GeneralEvents.OnHideSpinner -= HideSpinner;
			//GeneralEvents.OnShowSpinner -= ShowSpinner;
		}

		void OnLevelWasLoaded(int levelNumber)
		{
			HideSpinner ();
		}

		void HideSpinner()
		{
			Container.SetActive (false);
		}

		void ShowSpinner()
		{
			Container.SetActive (true);
		}
	}
}