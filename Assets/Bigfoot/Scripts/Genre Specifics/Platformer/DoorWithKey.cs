using UnityEngine;
using System.Collections;

public class DoorWithKey : RespawnableObject {

	public SpriteRenderer DoorRune;
	public Sprite StartingRune;
	public bool StartsOpened = false;
	public bool ShouldCloseAutomatically = false;
	public float AutoCloseAfter = 5f;
	public AudioClip openDoorSound;
	public AudioClip closeDoorSound;
	private Animator anim;
	private GameObject runeUsedWithDoor;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		if(!StartsOpened)
		{
			anim.speed = 0;
		}
		else
		{
			anim.speed = 1;
			GetComponent<Collider2D>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenDoor(GameObject rune)
	{
		#if SOUND_MANAGER_PRO
		if (openDoorSound != null)
			SoundManager.PlaySFX(openDoorSound);
#endif
		runeUsedWithDoor = rune;
		Sprite sprite = runeUsedWithDoor.GetComponent<SpriteRenderer>().sprite;
		DoorRune.sprite = sprite;
		GetComponent<Collider2D>().enabled = false;
		// maybe run ugly animation
		anim.Play(null, 0, 0);
		anim.speed = 1;
		if(ShouldCloseAutomatically)
			StartCoroutine(CloseAfterSeconds(AutoCloseAfter));
	}

	private IEnumerator CloseAfterSeconds(float secs)
	{
		yield return new WaitForSeconds(secs);
		runeUsedWithDoor.SetActive(true);
		CloseDoor();
	}

	public void CloseDoor()
	{
		#if SOUND_MANAGER_PRO
		if (closeDoorSound != null)
			SoundManager.PlaySFX(closeDoorSound);
#endif
		DoorRune.sprite = StartingRune;
		GetComponent<Collider2D>().enabled = true;
		anim.Play(null,0,1);
		anim.speed = -1;
	}

	#region implemented abstract members of RespawnableObject

	public override void ReturnToStartingPosition ()
	{
		DoorRune.sprite = StartingRune;
	}

	#endregion
}
