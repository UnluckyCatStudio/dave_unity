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
			EditorGUI.BeginChangeCheck ();

			// Change language
			t.language = ( Language ) EditorGUILayout.EnumPopup ( "Language", t.language );

			// Initialize texts array
			if ( t.texts == null )
				t.texts = new string[keys.Length];

			if ( t.texts.Length != keys.Length )
			{
				var newT = new string[keys.Length];
				for ( var i=0; i!=keys.Length; i++ )
				{
					if ( i < t.texts.Length )	newT[i] = t.texts[i];
					else						newT[i] = "";
				}

				t.texts = newT;
			}

			foreach ( int k in keys )
			{
				EditorGUILayout.LabelField ( keys[k].ToString () );
				t.texts[k] = EditorGUILayout.TextArea ( t.texts[k] );
			}

			if ( EditorGUI.EndChangeCheck () )
			{
				EditorUtility.SetDirty ( target );
			}
		}
	} 
}
