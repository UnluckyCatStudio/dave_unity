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

	/// <summary>
	/// Global UI parent.
	/// </summary>
	public static GameObject ui;

	private void Start ()
	{
		var lang	        = PlayerPrefs.GetInt    ( "Lang" );
		var jsonGraphics    = PlayerPrefs.GetString ( "Graphics" );
		var jsonInput       = PlayerPrefs.GetString ( "Input" );
		var jsonAudio       = PlayerPrefs.GetString ( "Audio" );

		#region TRANSLATION
		Localization.lang = lang;
		Localization.LoadTexts ();
		Localization.InitAllTexts ();
		Localization.UpdateAllTexts ();
		#endregion

		#region LOAD GRAPHICS
		var gManager = ui.GetComponentInChildren <GraphicsManager> ( true );
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
		var iManager = ui.GetComponentInChildren<InputManager> ( true );
		if ( jsonInput != "" )
			input = JsonUtility.FromJson<InputSettings> ( jsonInput );
        else
            input.SetDefaults ();

		iManager.LoadValues ();
		#endregion

		#region LOAD AUDIO
		var aManager = ui.GetComponentInChildren<AudioManager> ( true );
		if ( jsonAudio != "" )
			audio = JsonUtility.FromJson<AudioSettings> ( jsonAudio );
        else
            audio.SetDefaults ();

        // Initialize all values
        foreach ( var s in ui.GetComponentsInChildren<SliderInt> ( true ) )
            s.Init ();
        
		aManager.LoadValues ();
		aManager.ApplySave ( true );
		#endregion
	}

	private void Awake () 
	{
		ui = GameObject.Find ( "UI" );
	}
}
