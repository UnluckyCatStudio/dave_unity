using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class PauseManager : MonoBehaviour
{
	[HideInInspector]
	public bool paused = false;

	/// <summary>
	/// If false, prevents game from pausing
	/// </summary>
	public bool canPause = true;

	[Header("Menu animation")]
	public Texture2D[] imgs;
	public AnimationCurve animation;

	// Change game state
	public void SwitchPause ()
	{
		paused = !paused;
		StartCoroutine ( "PlayAnimation" );
	}

	// Plays animations ( backwards if de-pausing )
	//  -Shows main pause-menu object
	// - Slowly freezes time
	//		( time starts freezing before animation plays
	//		  cause looks cooler )
	IEnumerator PlayAnimation ()
	{
		// Prevent from pausing / de-pausing
		// while playing animation
		canPause = false;
		yield return null;
	}

	private void Update ()
	{
		if ( canPause && Input.GetKeyDown ( KeyCode.P ) )
			SwitchPause ();
	}
}
