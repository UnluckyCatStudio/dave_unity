using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct InputSettings 
{
	public KeyCode[] keys;
}

public enum Key 
{
	Forward,
	Backwards,
	Left,
	Right,
	Jump,
	Run,
	Sword,
	Boomerang,
	Lock,
	Attack,
	Interact
}

public class InputManager : MonoBehaviour
{
	#region KEYS
	public HotkeyButton[] hotkeys;
	#endregion

	public HotkeyButton selected;

	public void LoadValues ()
	{
		for ( int k=0; k!=hotkeys.Length; k++ )
		{
			hotkeys[k].info.text = Game.input.keys[(int)hotkeys[k].key].ToString ();
		}
	}

	public void ApplySave ()
	{
		for ( int k = 0; k != hotkeys.Length; k++ )
		{
			Game.input.keys[(int)hotkeys[k].key] = ParseKey ( hotkeys[k].info.text );
		}

		PlayerPrefs.SetString ( "Input", JsonUtility.ToJson ( Game.input ) );
		PlayerPrefs.Save ();
	}

	private KeyCode ParseKey ( string key )
	{
		KeyCode code;
		code = ( KeyCode ) Enum.Parse ( typeof ( KeyCode ), key, true );
		return code;
	}

	public void SelectButton ( HotkeyButton button )
	{
		selected = button;
		button.esc.gameObject.SetActive ( true );
		button.info.gameObject.SetActive ( false );
	}

	void Update ()
	{
		if ( selected != null )
		{
			foreach ( KeyCode kcode in Enum.GetValues ( typeof ( KeyCode ) ) )
			{
				if ( Input.GetKeyDown ( kcode ) )
				{
					// Check if user pressed ESC
					if ( kcode != KeyCode.Escape )
					{
						// Save new value
						Game.input.keys[(int)selected.key] = kcode;

						// Show new value
						selected.info.text = kcode.ToString ();
					}

					// Stop waiting input
					selected.info.gameObject.SetActive ( true );
					selected.esc.gameObject.SetActive ( false );
					selected = null;
				}
			}
		}
	}
}
