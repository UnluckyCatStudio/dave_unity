using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Kyru.UI;

[CustomEditor(typeof(Translation))]
public class Editor_Translation : Editor
{
	public override void OnInspectorGUI ()
	{
		var target = this.target as Translation;

		if ( Localization.texts == null )
		{
			Localization.InitAllTexts ();
		}
	}
}
