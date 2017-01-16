using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class PauseManager : MonoBehaviour
{
	[HideInInspector]
	public bool paused = false;

	// Change game state
	public void SwitchPause ()
	{
		paused = !paused;
		Camera.main.GetComponent<BlurOptimized> ().enabled = paused;
		Game.ui.pauseMenu.SetActive ( paused );
		Time.timeScale = ( paused ? 0 : 1 );
		
	}

	private void Update ()
	{
		if ( Input.GetKeyUp ( Game.input.pauseKey ) || Input.GetKeyUp ( Game.input.pauseKey2 ) )
			SwitchPause ();
	}
}
