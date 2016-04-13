using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Bigfoot
{
    [CustomEditor(typeof(UnityItem))]
    public class UnityItemEditor : Editor
    {

        private UnityItem item;
        //TODO: We should see if we can replace this for a property drawer

        // Use this for initialization
        public override void OnInspectorGUI()
        {
            item = (UnityItem)target;
            item.ItemId = (BFKItemKeys)EditorGUILayout.EnumPopup("Item Id", item.ItemId);
            item.item.Key = item.ItemId.ToString();
            item.item.Quantity = EditorGUILayout.IntField("Amount", item.item.Quantity);
            item.ItemLabel = (TextMesh)EditorGUILayout.ObjectField(item.ItemLabel, typeof(TextMesh), true);
#if NGUI
		item.NGUIItemLabel = (UILabel)EditorGUILayout.ObjectField(item.NGUIItemLabel, typeof(UILabel), true);
#endif
            if (GUI.changed)
                EditorUtility.SetDirty(target);
        }
    }
}