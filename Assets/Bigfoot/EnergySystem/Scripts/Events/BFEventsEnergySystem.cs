using System;

namespace Bigfoot
{
    public class BFEventsEnergySystem
    {
        public static Action<int, int> OnEnergyWon;

        public static void EnergyWon(int amountEnergy, int barId)
        {
            if (OnEnergyWon != null)
                OnEnergyWon(amountEnergy, barId);
        }

        public static Action<int, int> OnEnergyLost;

        public static void EnergyLost(int amountEnergy, int barId)
        {
            if (OnEnergyLost != null)
                OnEnergyLost(amountEnergy, barId);
        }
    }
}