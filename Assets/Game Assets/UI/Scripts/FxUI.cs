using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyru.UI
{
	/// <summary>
	/// General UI functions.
	/// </summary>
	public class FxUI : MonoBehaviour
	{
		public static bool paused = true;
		public Animator ui;

		public void Resume () 
		{

		}

		public void Play ()
		{
			ui.SetBool ( "OnMainMenu", false );
			// TODO: Loading scene!
		}

		public void QuitToMainMenu () 
		{

		}

		public void QuitToDesktop () 
		{
			Application.Quit ();
		}

		private void Update ()
		{
			if ( Input.GetKeyDown ( KeyCode.P ) )
				Play ();

			if ( !paused && Input.GetKeyDown ( KeyCode.P ) || Input.GetKeyDown ( KeyCode.Escape ) )
			{
				paused = true;
				ui.SetBool ( "Paused", paused );
			}
		}
	} 
}
