using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    /// <summary>
    /// Unity item. Is basically an Item, with a key and a quantity value. But with some helpers and UI Management for Unity
    /// </summary>
    public class UnityItem : MonoBehaviour
    {

        /// <summary>
        /// The item identifier. We'll use the Constants enum to avoid having mistakes with keys. If you want to add a new type of item. Add it in the Constants script
        /// </summary>
        public BFKItemKeys ItemId;

        /// <summary>
        /// The item.
        /// </summary>
        public Item item;

        /// <summary>
        /// The item label. If null, it won't be used after changing the value. And NGUI label will be used if possible
        /// </summary>
        public TextMesh ItemLabel;

        /// <summary>
        /// The NGUI item label. If null, it won't be used after changing the value
        /// </summary>
#if NGUI
	public UILabel NGUIItemLabel;
#endif
    }
}