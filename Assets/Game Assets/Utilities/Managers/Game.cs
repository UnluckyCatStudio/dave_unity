using System.Collections;
using System.Collections.Generic;
using Kyru.UI;
using UnityEngine;

/// <summary>
/// Contains static references
/// tp all managers.
/// </summary>
public class Game : MonoBehaviour
{
	public static GraphicSettings	 graphics = new GraphicSettings ();
	public static InputSettings      input    = new InputSettings ();
	public static new AudioSettings  audio    = new AudioSettings ();

	private void Awake ()
	{
		var lang	        = PlayerPrefs.GetInt    ( "Lang" );
		var jsonGraphics    = PlayerPrefs.GetString ( "Graphics" );
		var jsonInput       = PlayerPrefs.GetString ( "Input" );
		var jsonAudio       = PlayerPrefs.GetString ( "Audio" );

		#region TRANSLATION
		Localization.lang = lang;
		Localization.LoadTexts ();
		Localization.UpdateAllTexts ();
		#endregion

		#region LOAD GRAPHICS
		var gManager = FindObjectOfType<GraphicsManager> ();
		if ( jsonGraphics != "" )
			graphics = JsonUtility.FromJson<GraphicSettings> ( jsonGraphics );
		else
			graphics.SetDefaults ();

		#if !UNITY_EDITOR
		gManager.LoadResolutions ();
		#endif

		gManager.LoadValues ();
		gManager.ApplySave ( true );
		#endregion

		#region LOAD INPUT
		var iManager = FindObjectOfType<InputManager> ();
		if ( jsonInput != "" )
			input = JsonUtility.FromJson<InputSettings> ( jsonInput );

		iManager.LoadValues ();
		#endregion

		#region LOAD AUDIO
		var aManager = FindObjectOfType<AudioManager> ();
		if ( jsonAudio != "" )
			audio = JsonUtility.FromJson<AudioSettings> ( jsonAudio );

		aManager.LoadValues ();
		aManager.ApplySave ( true );
		#endregion
	}
}
