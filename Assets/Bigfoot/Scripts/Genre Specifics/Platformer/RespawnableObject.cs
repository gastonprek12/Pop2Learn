using UnityEngine;
using System.Collections;

public abstract class RespawnableObject : MonoBehaviour {

	protected Vector3 startingPosition;

	void Awake()
	{
		if(startingPosition == new Vector3(0,0,0))
		{
			startingPosition = gameObject.transform.position;
		}
	}

	public abstract void ReturnToStartingPosition ();
}
