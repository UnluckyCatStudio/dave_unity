using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains static references
/// tp all managers.
/// </summary>
public class Game : MonoBehaviour
{
	public static UIManager			mUI;
	public static GraphicsManager	mGraphics;
	public static AudioManager      mAudio;
	public static InputManager		mInput;
	public static PauseManager		mPause;

	// Initialize managers
	private void Awake ()
	{
		mUI			=	GetComponent<UIManager> ();
		mPause		=	GetComponent<PauseManager> ();
	}

	// Load settings
	private void Start ()
	{
		var jsonGraphics	= PlayerPrefs.GetString ( "Graphics" );
		var jsonAudio		= PlayerPrefs.GetString ( "Audio" );
		var jsonInput		= PlayerPrefs.GetString ( "Input" );

		if ( jsonGraphics != "" )	mGraphics = JsonUtility.FromJson<GraphicsManager> ( jsonGraphics );
		else						mGraphics = new GraphicsManager ();

		if ( jsonAudio != "" )	mAudio = JsonUtility.FromJson<AudioManager> ( jsonAudio );
		else					mAudio = new AudioManager ();

		if ( jsonInput != "" )	mInput = JsonUtility.FromJson<InputManager> ( jsonInput );
		else					mInput = new InputManager ();
	}
}
