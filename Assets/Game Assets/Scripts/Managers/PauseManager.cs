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
		Game.mUI.pauseMenu.SetActive ( paused );
		Time.timeScale = ( paused ? 0 : 1 );
		
	}

	private void Update ()
	{
		if ( Input.GetKeyUp ( Game.mInput.pauseKey ) || Input.GetKeyUp ( Game.mInput.pauseKey2 ) )
			SwitchPause ();
	}
}
