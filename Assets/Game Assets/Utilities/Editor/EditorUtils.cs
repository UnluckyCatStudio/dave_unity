﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorUtils : Editor
{
	[MenuItem("etc-utils/Player prefs/Delete all player prefs")]
	public static void DeletePlayerPrefs ()
	{
		PlayerPrefs.DeleteAll ();
		Debug.Log ( "All PlayerPrefs deleted!" );
	}

	[MenuItem( "etc-utils/Player prefs/Print all" )]
	public static void PrintPlayerPrefs ()
	{
		Debug.Log ( "Graphics : " + PlayerPrefs.GetString ( "Graphics" ) );
		Debug.Log ( "Audio : " + PlayerPrefs.GetString ( "Audio" ) );
		Debug.Log ( "Input : " + PlayerPrefs.GetString ( "Input" ) );
		Debug.Log ( "Lang : " + PlayerPrefs.GetInt ( "Lang" ) );
	}

	[MenuItem("etc-utils/Localization/Save default lang texts")]
	public static void SaveLangTextToDisk ()
	{
		var path = Application.dataPath + "/Lang/ref.lang";

		using ( var file = new System.IO.StreamWriter ( path, false ) )
		{
			foreach ( var t in Localizator.PrintTexts () )
				file.WriteLine ( t );
		}

		Debug.Log ( "Language-file reference saved in " + path );
	}
}
