using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	// Changed in save-file
	public KeyCode pauseKey		= KeyCode.P;
	public KeyCode pauseKey2	= KeyCode.Escape;
	public KeyCode run			= KeyCode.LeftShift;

	// Changed through menu
	public KeyCode forward				= KeyCode.D;
	public KeyCode backwards            = KeyCode.A;
	public KeyCode jump					= KeyCode.Space;
	public KeyCode backwards_jump		= KeyCode.Space;

	public void LoadValues ()
	{
		Game.ui.forward.text			= forward.ToString ();
		Game.ui.backwards.text			= backwards.ToString ();
		Game.ui.jump.text				= jump.ToString ();
		Game.ui.backwards_jump.text		= backwards_jump.ToString ();
	}

	public void ApplySave ()
	{
		forward			= ParseKey ( Game.ui.forward.text );
		backwards		= ParseKey ( Game.ui.backwards.text );
		jump			= ParseKey ( Game.ui.jump.text );
		backwards_jump	= ParseKey ( Game.ui.backwards_jump.text );

		PlayerPrefs.SetString ( "Input", JsonUtility.ToJson ( this ) );
		PlayerPrefs.Save ();
	}

	private KeyCode ParseKey ( string key )
	{
		KeyCode code;
		code = ( KeyCode ) System.Enum.Parse ( typeof ( KeyCode ), key, true );
		return code;
	}
}
