using UnityEngine;
using System.Collections;
using Bigfoot;

public class GiveXpOnClick : MonoBehaviour {

	public int XpAmount;

	void OnClick()
	{
		BFEventsUserLevel.XpWon (XpAmount);
	}
}
