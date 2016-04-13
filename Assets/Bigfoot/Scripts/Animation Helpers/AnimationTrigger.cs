using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class AnimationTrigger : MonoBehaviour
    {
#if NGUI
	public enum TriggerType
	{
		Float,
		Int,
		Bool,
		Trigger,
		StartingAnimation
	}

	public Animator Anim;
	public string AnimationName;
	public TriggerType Type;
	public float FloatValue;
	public int IntegerValue;
	public bool BoolValue;

	public bool PlayOnAwake;
	public bool PingPongEffect;

	public GameObject blackScreen;

	public AudioClip soundDown;
	public AudioClip soundUp;

	private bool blackScreenActive = false;
	private bool open = false;
	private TweenAlpha tween;
	private bool ShouldPlayForward = true;
	private bool inCollider = false;


	void OnEnable()
	{
		if(!Anim)
			Anim = GetComponent<Animator>();
		if(!PlayOnAwake)
		{
			Anim.speed = 0;
			ShouldPlayForward = true;
		}
		else
		{
			ShouldPlayForward = false;
		}
		Anim.Play(AnimationName,0,0);
		if (blackScreen!= null){
			if (tween == null)
				tween = blackScreen.GetComponent<TweenAlpha> ();
			blackScreenActive = false;
			blackScreen.SetActive (blackScreenActive);
			if (blackScreenActive) {
				tween.PlayForward();
			}
			else{
				tween.ResetToBeginning();
			}
		}
		open = false;
	}

	void OnMouseEnter(){
		inCollider = true;
	}

	void OnMouseExit(){
		inCollider = false;
	}

	void OnMouseUp ()
	{
		if (inCollider)
			OnClick();
	}

	public virtual void OnClick()
	{
		switch(Type)
		{
			case(TriggerType.Float):
				Anim.SetFloat(AnimationName,FloatValue);
			break;
			case(TriggerType.Int):
				Anim.SetInteger(AnimationName,IntegerValue);
			break;
			case(TriggerType.Bool):
				Anim.SetBool(AnimationName,BoolValue);
			break;
			case(TriggerType.Trigger):
				Anim.SetTrigger(AnimationName);
			break;
			case(TriggerType.StartingAnimation):
				if(PingPongEffect && ShouldPlayForward)
				{
					Anim.speed = 1;
					AnimatorStateInfo info = Anim.GetCurrentAnimatorStateInfo(0);
					if(info.normalizedTime < 0)
					{
						Anim.Play(AnimationName,0,0);
					}
					else
					{
						Anim.Play(AnimationName,0,info.normalizedTime);
					}
					ShouldPlayForward = false;
				}
				else if(PingPongEffect && !ShouldPlayForward)
				{
					Anim.speed = -1;
					AnimatorStateInfo info = Anim.GetCurrentAnimatorStateInfo(0);					
					if(info.normalizedTime > 1)
					{
						Anim.Play(AnimationName,0,1);
					}
					else
					{
						Anim.Play(AnimationName,0,info.normalizedTime);
					}
					ShouldPlayForward = true;
				}
				else
				{
					Anim.speed = 1;
				}
			break;
		}
		if (blackScreen != null){
			if (tween == null)
				tween = blackScreen.GetComponent<TweenAlpha> ();
			blackScreenActive = !blackScreenActive;
			blackScreen.SetActive (blackScreenActive);
			if (blackScreenActive) {
				tween.PlayForward();
			}
			else{
				tween.ResetToBeginning();
			}
		}
		open = !open;
	}
#endif
    }
}