using UnityEngine;
using System.Collections;
using Bigfoot;


public class DecreaseEnergyOnClick : MonoBehaviour
{

    public int BarId;

    void OnClick()
    {
        BFEventsEnergySystem.EnergyLost(1, BarId);
    }
}