using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kyru.etc;

namespace Kyru.UI
{
	// General UI functions.
	public class FxUI : MonoBehaviour
	{
		public void Resume () 
		{
			Game.ui.SetBool ( "Paused", false );
			Cursor.visible = true;
			Time.timeScale = 1;
		}

		// Starts new game
		public void Play ()
		{
			Game.ui.SetBool ( "OnMainMenu", false );
			Game.ui.SetBool ( "Loading", true );
			Cursor.visible = false;
			ZoneLoader.Init ( this, "Zona_0", 3f );
		}

		public void QuitToMainMenu () 
		{
			Game.ui.SetBool ( "OnMainMenu", true );
			Game.ui.SetBool ( "Paused", false );
			Cursor.visible = true;
		}

		public void QuitToDesktop () 
		{
			Application.Quit ();
		}


		private bool canPause 
		{
			get
			{
				return
					! ( Game.ui.GetBool ( "OnMainMenu" )
					 || Game.ui.GetBool ( "Loading" ) );
			}
		}
		public static bool paused = true;
		void Update ()
		{
			// Animator check
			paused = Game.ui.GetBool ( "Paused" );

			if ( canPause && Input.GetKeyDown ( KeyCode.Escape ) )
			{
				Game.ui.SetBool ( "Paused", !paused );
				Cursor.visible = paused;
				Time.timeScale = !paused ? 0 : 1;
			}
		}

		// Don't destoy on load
		private void Awake () 
		{
			DontDestroyOnLoad ( this );
		}
	} 
}
