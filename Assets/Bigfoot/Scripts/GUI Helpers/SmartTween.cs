using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class SmartTween : MonoBehaviour
    {
#if NGUI
	public UITweener[] Tweens;

	public GameObject[] Fxs;

	public string SoundSfx;

	public bool NotifyController = false;

	void OnClick()
	{
        PlayTween();
	}

    public void PlayTween()
    {
		bool notifiedController = false;
		foreach (UITweener Tween in Tweens)
		{
	        if (Tween.tweenFactor == 0)
	        {
	            Tween.PlayForward();
				if(NotifyController && !notifiedController)
				{
					notifiedController = true;
					//HudController.Instance.UserOpenedPanel(this.gameObject);
				}
	            if (Fxs.Length > 0)
	                StartCoroutine(ActivateFXForSeconds(1f));
	        }
	        else
	        {
	            Tween.PlayReverse();
	            if (Fxs.Length > 0)
	                StartCoroutine(ActivateFXForSeconds(1f));
	        }
		}
    }

	public void PlayForward()
	{
		bool notifiedController = false;
		foreach (UITweener Tween in Tweens)
		{
			if (Tween.tweenFactor == 0)
			{
				Tween.PlayForward();
				if(NotifyController && !notifiedController)
				{
					notifiedController = true;
					//HudController.Instance.UserOpenedPanel(this.gameObject);
				}
				if (Fxs.Length > 0)
					StartCoroutine(ActivateFXForSeconds(1f));
			}
		}
	}

	public void PlayReverse()
	{
		foreach (UITweener Tween in Tweens)
		{

				Tween.PlayReverse();
				if (Fxs.Length > 0)
					StartCoroutine(ActivateFXForSeconds(1f));

		}
	}

    IEnumerator ActivateFXForSeconds(float secs)
    {
		foreach(GameObject go in Fxs)
        	go.SetActive(true);
#if SOUND_MANAGER_PRO
        SoundManager.PlaySFX(SoundSfx);
#endif
        yield return new WaitForSeconds(secs);
		foreach(GameObject go in Fxs)
			go.SetActive(false);
    }

	/*void OnDisable()
	{
		foreach(UITweener Tween in Tweens)
		{
			Tween.ResetToBeginning ();
			foreach (GameObject go in Fxs)
				go.SetActive (false);
		}
	}*/
#endif
    }

}