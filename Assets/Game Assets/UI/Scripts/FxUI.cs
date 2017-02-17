using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyru.UI
{
	// General UI functions.
	public class FxUI : MonoBehaviour
	{
		public static bool paused = true;
		Animator ui;

		public void Resume () 
		{

		}

		// Starts new game
		public void Play ()
		{
			ui.SetBool ( "OnMainMenu", false );
			ui.SetBool ( "Loading", true );
			ZoneLoader.Init ( this, "Zona_0", 3f );
		}

		public void QuitToMainMenu () 
		{

		}

		public void QuitToDesktop () 
		{
			Application.Quit ();
		}

		// Don't destoy on load
		private void Awake () 
		{
			DontDestroyOnLoad ( this );
			ui = GetComponent<Animator> ();
		}
	} 
}
