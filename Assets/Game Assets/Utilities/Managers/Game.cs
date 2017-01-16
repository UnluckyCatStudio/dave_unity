using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains static references
/// tp all managers.
/// </summary>
public class Game : MonoBehaviour
{
	public static UIManager			ui;
	public static GraphicsManager	graphics;
	public static AudioManager      audio;
	public static InputManager		input;
	public static PauseManager		pause;

	// Initialize managers
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
		var jsonGraphics	= PlayerPrefs.GetString ( "Graphics" );
		var jsonAudio		= PlayerPrefs.GetString ( "Audio" );
		var jsonInput		= PlayerPrefs.GetString ( "Input" );

		if ( jsonGraphics != "" )	graphics	= JsonUtility.FromJson<GraphicsManager> ( jsonGraphics );
		graphics.LoadResolutions ();
		graphics.LoadValues ();

		if ( jsonAudio != "" )		audio		= JsonUtility.FromJson<AudioManager> ( jsonAudio );
		

		if ( jsonInput != "" )		input		= JsonUtility.FromJson<InputManager> ( jsonInput );
		input.LoadValues ();
	}
}
