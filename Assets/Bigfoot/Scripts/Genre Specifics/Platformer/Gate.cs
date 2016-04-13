using UnityEngine;
using System;
using System.Collections;

public class Gate : MonoBehaviour
{
	public Action<Gate> OnGateActivated;
	public bool isActivated;
	public float ActivationDelay = 1f;
	public AudioClip sound;
	private bool CanBeActivated = true;
	private Animator anim;
	private int _id;

	// Use this for initialization
	void Start ()
	{
		this.isActivated = false;
		anim = GetComponent<Animator>();
		anim.speed = 0;
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void setId (int id)
	{
		this._id = id;
	}

	public int getId ()
	{
		return _id;
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Box") {
			if (OnGateActivated != null && CanBeActivated) {
				OnGateActivated (this);
				anim.speed = 1;
				anim.Play("redtoblueButton",0,0);
				#if SOUND_MANAGER_PRO
				if (sound != null) {
					SoundManager.PlaySFX(sound);
				}
#endif
				StartCoroutine(MakeAvailableForActivationAfterSeconds(ActivationDelay));
			}

		}
	}


	private IEnumerator MakeAvailableForActivationAfterSeconds(float seconds)
	{
		CanBeActivated = false;
		yield return new WaitForSeconds(seconds);
		CanBeActivated = true;
	}

	public void DeactivateGate ()
	{
		anim.speed = -1;
		this.isActivated = false;
		CanBeActivated = true;
	}

}
