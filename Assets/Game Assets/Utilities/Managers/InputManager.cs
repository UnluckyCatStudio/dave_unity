using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct InputSettings
{
	public int[] keys;
}

public class InputManager : MonoBehaviour
{
	#region KEYS
	public KeyCode[] keys =
	{
		KeyCode.W,									// Forward
		KeyCode.S,									// Backwards
		KeyCode.A,									// Left
		KeyCode.D,									// Right
		KeyCode.Space,								// Jump
		KeyCode.LeftShift,							// Run
		KeyCode.R,									// Sword
		KeyCode.Q,									// Boomerang
		KeyCode.Mouse2,								// Lock enemy
		KeyCode.Mouse0,								// Attack
		KeyCode.E									// Interact
	};
	#endregion

	public void LoadValues ()
	{
		for ( int k=0; k!=keys.Length; k++ )
		{
			Game.ui.hotkeys[k].text = keys[k].ToString ();
		}
	}

	public void ApplySave ()
	{
		for ( int k = 0; k != keys.Length; k++ )
		{
			keys[k] = ParseKey ( Game.ui.hotkeys[k].text );
		}

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
