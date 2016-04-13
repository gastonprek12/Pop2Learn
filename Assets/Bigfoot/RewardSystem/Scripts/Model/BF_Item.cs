using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    [System.Serializable]
    public class BF_Item
    {
        public BF_Item(string name, BFKItemKeys itemKey, int amount)
        {
            Name = name;
            ItemKey = itemKey;
            ItemAmount = amount;
        }

        /// <summary>
        /// The name of the Reward. Used for editor purposes
        /// </summary>
        public string Name;

        /// <summary>
        /// The Reward Identifier key
        /// </summary>
        public BFKItemKeys ItemKey;

        /// <summary>
        /// Amount of the specified item
        /// </summary>
        public int ItemAmount = 1;
    }
}