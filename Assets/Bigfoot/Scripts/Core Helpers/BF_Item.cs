using UnityEngine;
using System.Collections;
using System;

namespace Bigfoot
{
    [Serializable]
    public class Item
    {
        /// <summary>
        /// The key for the item. Used to retrieve from the Item Manager. If you add an Item, add it's
        /// key to the Constants.cs enumeration
        /// </summary>
        public string Key;

        /// <summary>
        /// The quantity of this particular.
        /// </summary>
        public int Quantity;
    }
}