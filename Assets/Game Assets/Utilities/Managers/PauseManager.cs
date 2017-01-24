using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class PauseManager : MonoBehaviour
{
	/// <summary>
	/// If false, prevents game from pausing
	/// </summary>
	public static bool canPause = true;
	public static bool paused = false;

	//[Header("Time-freeze animation")]
	//public float duration;
	//public AnimationCurve curve;
	public int fps;
	public Sprite[] imgs;

	// Change game state
	public IEnumerator SwitchPause ()
	{
		// Prevent from pausing / de-pausing
		// while playing animation
		canPause = false;

		// Wait for time to freeze ( almost )
		//StartCoroutine ( "FreezeTime" );
		//yield return new WaitForSeconds ( duration * .75f );    // 75% duration

		// Play BG animation
		if ( paused )		// Hide every menu
		{
			Game.ui.pauseMenuMain.SetActive ( false );
			Game.ui.pauseMenuOptions.SetActive ( false );
			Game.ui.pauseMenuGraphics.SetActive ( false );
			Game.ui.pauseMenuAudio.SetActive ( false );
			Game.ui.pauseMenuControls.SetActive ( false );
		}
		else Game.ui.pauseMenu.SetActive ( true );		// Show BG

		var i = paused ? imgs.Length-1 : 0;
		while ( i != ( paused ? 0 : imgs.Length ) )
		{
			//print ( i );
			Game.ui.pauseMenuBG.sprite = imgs[i];
			i += paused ? -1 : +1;

			yield return new WaitForSeconds ( ( float ) 1 / fps );
		}
		if ( !paused )
		{
			Game.ui.pauseMenuMain.SetActive ( true );
			Game.ui.pauseMenu.SetActive ( true );
		}
		else Game.ui.pauseMenu.SetActive ( false );

		paused = !paused;
		canPause = true;
	}


	private void Update ()
	{
		if ( canPause && !paused && Input.GetKeyDown ( KeyCode.P ) )
			StartCoroutine ( "SwitchPause" );
	}

	#region FREEZE TIME ( WASTED )
	// im very sad cause i really wanted to do this
	// but it's just not possible.
	// bye.
	/*

	// Slowly freezes time
	//	( time starts freezing before animation plays
	//	  cause looks cooler )
	IEnumerator FreezeTime ()
	{
		// Animation value
		float t			= paused ? 0 : 1;	// If paused: time[0=>1]
		float target    = paused ? 1 : 0;
		while ( paused ? ( t <= target ) : ( t >= target ) )
		{
			var value = curve.Evaluate ( t );
			Time.timeScale = Mathf.Clamp01 ( t );

			var step = 1 / duration * Time.deltaTime;
			t += paused ? step : -step;
			//print ( "t: " + t + " - " + value );
			yield return null;
		}
	}
	*/
	#endregion
}
