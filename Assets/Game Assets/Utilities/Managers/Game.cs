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

	// Order is irrelevant here
	[SerializeField]
	private Translation[] translations;

	/// <summary>
	/// Global UI parent.
	/// </summary>
	public static Animator ui;

	/// <summary>
	/// Reference to the Main Camera Rig Manager
	/// </summary>
	public static CamManager cam;

	/// <summary>
	/// Global reference to Dave controller.
	/// </summary>
	public static DaveController dave;

	/// <summary>
	/// The fucking sun.
	/// </summary>
	public static Light sun;

	private void Awake ()
	{
		DontDestroyOnLoad ( this );

		var lang	        = PlayerPrefs.GetInt    ( "Lang" );
		var jsonGraphics    = PlayerPrefs.GetString ( "Graphics" );
		var jsonInput       = PlayerPrefs.GetString ( "Input" );
		var jsonAudio       = PlayerPrefs.GetString ( "Audio" );

		ui  = GameObject.Find ( "UI" ).GetComponent<Animator> ();
		// Camera Rig will be self-set when game is started
		// Initialize all sliders
		foreach (var s in ui.GetComponentsInChildren<SliderInt> ( true ))
			s.Init ();

		#region TRANSLATION
		// Setup references
		foreach ( var t in translations )
		{
			// Order independent
			var i = ( int ) t.language;
			Localization.translations[i] = t;
		}

		Localization.lang = lang;
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
        
		aManager.LoadValues ();
		aManager.ApplySave ( true );
		#endregion
	}
}
