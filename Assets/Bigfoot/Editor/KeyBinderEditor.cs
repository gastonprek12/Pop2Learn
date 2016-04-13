using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(KeyBinder))]
public class KeyBinderEditor : Editor
{
	KeyBinder mBinder;
	
	void OnEnable ()
	{
		mBinder = target as KeyBinder;
		EditorPrefs.SetBool("ET0", EventDelegate.IsValid(mBinder.Targets));
	}
	
	public override void OnInspectorGUI ()
	{
		GUILayout.Space(3f);
		NGUIEditorTools.SetLabelWidth(80f);
		bool minimalistic = NGUISettings.minimalisticLook;
		mBinder.Key = (KeyCode) EditorGUILayout.EnumPopup("Key", mBinder.Key);

		
		DrawEvents("ET0", "Key Binder Target", mBinder.Targets, minimalistic);
	}
	
	void DrawEvents (string key, string text, List<EventDelegate> list, bool minimalistic)
	{
		if (!NGUIEditorTools.DrawHeader(text, key, false, minimalistic)) return;
		NGUIEditorTools.BeginContents();
		EventDelegateEditor.Field(mBinder, list, null, null, minimalistic);
		NGUIEditorTools.EndContents();
	}
}
