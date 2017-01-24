using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains static references
/// tp all managers.
/// </summary>
public class Game : MonoBehaviour
{
	public static UIManager         ui;
	public static GraphicsManager   graphics;
	public static AudioManager      audio;
	public static InputManager      input;
	public static PauseManager      pause;

	// Initialize managers ( default values )
	private void Awake ()
	{
		ui			= GetComponent<UIManager> ();
		graphics	= GetComponent<GraphicsManager> ();
		audio		= GetComponent<AudioManager> ();
		input		= GetComponent<InputManager> ();
		pause		= GetComponent<PauseManager> ();
	}

	// Load settings
	private void Start ()
	{
		var jsonLang        = PlayerPrefs.GetInt    ( "Lang" );
		var jsonGraphics    = PlayerPrefs.GetString ( "Graphics" );
		var jsonAudio       = PlayerPrefs.GetString ( "Audio" );
		var jsonInput       = PlayerPrefs.GetString ( "Input" );

		#region TRANSLATION
		ui.language.value = jsonLang;
		Localizator.Initialize ();
		string[] availableLangs =
		{
			"en",	// English
			"es",	// Spanish
			"cat"	// Catalan
		};
		Localizator.LoadLang ( availableLangs[( int ) ui.language.value] );
		Localizator.UpdateAll ();
		#endregion

		#region LOAD GRAPHICS
		if ( jsonGraphics != "" )
		{
			var temp = JsonUtility.FromJson<GraphicsSettings> ( jsonGraphics );

			graphics.resolution		= temp.resolution;
			graphics.fullscreen		= temp.fullscreen;
			graphics.vsync			= temp.vsync;
			graphics.textures		= temp.textures;
			graphics.postFX			= temp.postFX;
			graphics.shadows		= temp.shadows;
			graphics.fov			= temp.fov;
			graphics.antialising	= temp.antialising;
		}
#if !UNITY_EDITOR
		graphics.LoadResolutions ();
#endif
		graphics.LoadValues ();
		graphics.ApplySave ( true );
		#endregion

		#region LOAD AUDIO
		if ( jsonAudio != "" )
		{
			var temp = JsonUtility.FromJson<AudioSettings> ( jsonAudio );

			audio.master	= temp.master;
			audio.music		= temp.music;
			audio.sfx		= temp.sfx;
			audio.ambient	= temp.ambient;
			audio.voices	= temp.voices;
		}
		audio.LoadValues ();
		audio.ApplySave ( true );
		#endregion

		#region LOAD INPUT
		if ( jsonInput != "" )
		{
			var temp = JsonUtility.FromJson<InputSettings> ( jsonInput );

			for ( int i = 0; i != temp.keys.Length; i++ )
				input.keys[i] = ( KeyCode ) temp.keys[i];
		}
		input.LoadValues ();
		// No need to apply, since game 
		// will search itself for
		// the controls here
		#endregion
	}
}
