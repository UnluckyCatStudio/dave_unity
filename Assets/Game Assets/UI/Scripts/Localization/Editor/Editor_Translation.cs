using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kyru.UI
{
	[CustomEditor ( typeof ( Translation ) )]
	public class Editor_Translation : Editor
	{
		Translation t;
		AllTexts[] keys = ( AllTexts[] ) Enum.GetValues ( typeof ( AllTexts ) );
		public override void OnInspectorGUI ()
		{
			t = target as Translation;

			// Initialize array
			if ( t.texts == null || t.texts.Length != keys.Length )
				t.texts = new string[keys.Length];

			foreach ( int k in keys )
			{
				t.texts[k] = EditorGUILayout.TextField ( keys[k].ToString (), t.texts[k] );
			}
		}
	} 
}
