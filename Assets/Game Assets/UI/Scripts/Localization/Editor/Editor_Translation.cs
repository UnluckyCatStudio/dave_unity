using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Kyru.UI;

[CustomEditor(typeof(Translation))]
public class Editor_Translation : Editor
{
    Translation t;
	public override void OnInspectorGUI ()
	{
		t = target as Translation;
        InitializationCheck ();

		foreach ( var txt in Localization.texts )
		{
			// Temp array of all texts
			var list = new List<string> ();

		}
	}

    // Checks if Localization
    // dictionaries are initialized.ç
    // If not, do so.
    void InitializationCheck () 
    {
		if (Localization.registry == null)
		{
			Localization.InitAllTexts();
		}

        if (Localization.texts == null)
        {
            Localization.lang = t.language;
			Localization.translations[(int)t.language] = t;
			Localization.LoadTexts ();
		}

    }
}
