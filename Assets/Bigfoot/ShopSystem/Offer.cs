using UnityEngine;
using System.Collections;
using System;

namespace Bigfoot
{

    [Serializable]
    public class Offer
    {
        public string Name;
        public string Description;
        public BFKIAPType Type;
        public BFKStoreIds StoreId;
        public BFKItemKeys ItemId;
        public int Amount;
        public Value[] Costs;

        public float GetCost(string currency)
        {
            foreach (Value v in Costs)
            {
                if (v.Currency.ToString() == currency)
                    return v.Cost;
            }

            return 0;
        }
    }
}