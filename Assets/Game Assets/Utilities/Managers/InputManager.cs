using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kyru.UI;

[Serializable]
public struct InputSettings
{
	public KeyCode[] keys;

	public bool invertX;
	public bool invertY;

	#region FX
	public bool GetKey ( Key key )
	{
		return Input.GetKey ( keys[( int ) key] );
	}

	public bool GetKeyDown ( Key key )
	{
		return Input.GetKeyDown ( keys[( int ) key] );
	}

	public bool GetKeyUp ( Key key )
	{
		return Input.GetKeyUp ( keys[( int ) key] );
	} 
	#endregion

	public void SetDefaults () 
    {
        keys = new KeyCode[]
        {
            KeyCode.W,
            KeyCode.S,
            KeyCode.A,
            KeyCode.D,
            KeyCode.Space,
            KeyCode.LeftShift,
            KeyCode.R,
            KeyCode.Mouse2,
            KeyCode.Mouse0,
            KeyCode.E,
			KeyCode.Q
		};

		invertX = false;
		invertY = false;
    }
}

public enum Key 
{
	Forward,
	Backwards,
	Left,
	Right,
	Jump,
	Walk,
	Sword,
	Boomerang,
	Attack,
	Interact,
	Shield
}

public class InputManager : MonoBehaviour
{
	#region UI
	public HotkeyButton[] hotkeys;

	public Toggle invertX;
	public Toggle invertY;
	#endregion

	HotkeyButton selected;

	public void LoadValues () 
	{
		for ( int k=0; k!=hotkeys.Length; k++ )
		{
			hotkeys[k].info.text = Game.input.keys[(int)hotkeys[k].key].ToString ();
		}
		
		invertX.isOn = Game.input.invertX;
		invertY.isOn = Game.input.invertY;
	}

	public void ApplySave () 
	{
		for ( int k = 0; k != hotkeys.Length; k++ )
		{
			Game.input.keys[(int)hotkeys[k].key] = ParseKey ( hotkeys[k].info.text );
		}

		Game.input.invertX = invertX.isOn;
		Game.input.invertY = invertY.isOn;

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
					// Check if user pressed ESC or P ( pausing keys )
					// Or  ENTER
					if (   kcode != KeyCode.Escape
						&& kcode != KeyCode.P
						&& kcode != KeyCode.Return )
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
