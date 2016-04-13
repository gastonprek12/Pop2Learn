using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public AudioClip wallSound;
	public bool ShouldAutoClose;
	public float SecondsBeforeClosingAgain;
	[HideInInspector]
	public bool IsOpen = false;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.speed = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivateDoor()
	{
		anim.Play(null, 0, 0);
		anim.speed = 1;
#if SOUND_MANAGER_PRO
		if (wallSound != null)
			SoundManager.PlaySFX(wallSound);
#endif
		IsOpen = true;
		if(ShouldAutoClose)
			StartCoroutine(CloseAfterSeconds(SecondsBeforeClosingAgain));
	}

	private IEnumerator CloseAfterSeconds(float secs)
	{
		yield return new WaitForSeconds(secs);
		anim.Play(null, 0, 1);
		anim.speed = -1;
		IsOpen = false;
	}
}
