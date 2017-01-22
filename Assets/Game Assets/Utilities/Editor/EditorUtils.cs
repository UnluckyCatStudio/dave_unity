using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorUtils : Editor
{
	[MenuItem("etc-utils/Player prefs/Delete all player prefs")]
	public static void DeletePlayerPrefs ()
	{
		PlayerPrefs.DeleteAll ();
	}

	[MenuItem( "etc-utils/Player prefs/Print all" )]
	public static void PrintPlayerPrefs ()
	{
		Debug.Log ( PlayerPrefs.GetString ( "Graphics" ) );
		Debug.Log ( PlayerPrefs.GetString ( "Audio" ) );
		Debug.Log ( PlayerPrefs.GetString ( "Input" ) );
	}

	[MenuItem("etc-utils/Localization/Save default lang texts")]
	public static void SaveLangTextToDisk ()
	{
		using ( var file = System.IO.File.CreateText ( Application.persistentDataPath + "/ref.lang" ) )
		{
			foreach ( var t in Localizator.PrintTexts () )
				file.WriteLine ( t );
		}
	}
}
