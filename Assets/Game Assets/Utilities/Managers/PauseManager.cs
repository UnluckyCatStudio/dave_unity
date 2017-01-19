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

	// Change game state
	public void SwitchPause ()
	{
		paused = !paused;
		Game.ui.pauseMenu.SetActive ( paused );
		Time.timeScale = ( paused ? 0 : 1 );
		
	}

	private void Update ()
	{
		if ( Input.GetKeyDown ( KeyCode.P ) )
			SwitchPause ();
	}
}
