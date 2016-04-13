using UnityEngine;
using System.Collections;

public class GateManager : MonoBehaviour
{
	public Gate[] Gates;
	public int[] GateOrder;
	public Door[] Doors;

	private int[] currentActivationOrder;
	private int numberOfGatesActivated;
	public float SecondsToSwitchDeactivation = 2f;

	void Awake ()
	{
		int i = 1;
		foreach (Gate gate in Gates) {
			gate.setId (i);
			gate.OnGateActivated += GateActivated;
			i++;
		}
	}

	// Use this for initialization
	void Start ()
	{
		if (Gates.Length != GateOrder.Length) {
			Debug.LogError ("La cantidad de gates y el orden en que se activan no coincide");
		}

		currentActivationOrder = new int[Gates.Length];
		numberOfGatesActivated = 0;
	}

	private void GateActivated (Gate gate)
	{
		if (gate.isActivated) {
			return;
		}
		gate.isActivated = true;
		//Debug.Log(string.Format("Activated door: {0}",gate.name));
		this.currentActivationOrder [numberOfGatesActivated] = gate.getId ();
		this.numberOfGatesActivated++;
		// If he stepped on every gate at least once
		if (numberOfGatesActivated == this.currentActivationOrder.Length) {
			bool wasCorrectOrder = true;
			for (int i = 0; i < this.Gates.Length; i++) {
				//if the activation order is not the same as the order he pressed
				if (this.currentActivationOrder [i] != this.GateOrder [i]) {
						wasCorrectOrder = false;
				}
				this.Gates [i].isActivated = false;
			}

			// we have to reset everything because it failed
			this.currentActivationOrder = new int[Gates.Length];
			this.numberOfGatesActivated = 0;

			if (wasCorrectOrder) {
				ActivateDoors ();
			} else {
				StartCoroutine(DeactivateSwitchesAfterSeconds(SecondsToSwitchDeactivation));
			}
		}
	}

	public void ActivateDoors ()
	{
		foreach (Door door in Doors) {
			if(!door.IsOpen)
				door.ActivateDoor ();
		}
		StartCoroutine(DeactivateSwitchesAfterSeconds(SecondsToSwitchDeactivation));
	}

	private IEnumerator DeactivateSwitchesAfterSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		foreach (Gate gateToDeactivate in this.Gates) {
			gateToDeactivate.DeactivateGate ();
		}
	}
}

