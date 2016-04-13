using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class KeyBinder : MonoBehaviour {

	public List<EventDelegate> Targets = new List<EventDelegate>();

    public KeyCode Key;

	void Update () 
	{
	    if(Input.GetKeyUp(Key))
        {
			EventDelegate.Execute(Targets);
        }
	}
}
