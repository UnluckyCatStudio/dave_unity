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

	public int speedX;
	public int speedY;

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
            KeyCode.W,				// Forward
            KeyCode.S,				// Backwards
            KeyCode.A,				// Left
            KeyCode.D,				// Right
            KeyCode.Space,			// Shield
            KeyCode.LeftShift,		// Walk
            KeyCode.R,				// Sheathe
            KeyCode.Mouse1,			// Attack-big
            KeyCode.Mouse0,			// Attack-single / Shot
            KeyCode.E,				// Interact
			KeyCode.Q				// Charge
		};

		invertX = false;
		invertY = false;

		speedX = 100;
		speedY = 100;
    }
}

public enum Key 
{
	Forward,
	Backwards,
	Left,
	Right,
	Shield,
	Walk,
	Sword,
	Attack_big,
	Attack_single,
	Interact,
	Charge
}

public class InputManager : MonoBehaviour
{
	#region UI
	public HotkeyButton[] hotkeys;
	HotkeyButton selected;

	public Toggle invertX;
	public Toggle invertY;

	public Slider speedX;
	public Slider speedY;
	#endregion

	public void LoadValues () 
	{
		for ( int k=0; k!=hotkeys.Length; k++ )
		{
			hotkeys[k].info.text = Game.input.keys[(int)hotkeys[k].key].ToString ();
		}
		
		invertX.isOn = Game.input.invertX;
		invertY.isOn = Game.input.invertY;

		speedX.value = Game.input.speedX;
		speedY.value = Game.input.speedY;
	}

	public void ApplySave () 
	{
		for ( int k = 0; k != hotkeys.Length; k++ )
		{
			Game.input.keys[(int)hotkeys[k].key] = ParseKey ( hotkeys[k].info.text );
		}

		Game.input.invertX = invertX.isOn;
		Game.input.invertY = invertY.isOn;

		Game.input.speedX = (int) speedX.value;
		Game.input.speedY = (int) speedY.value;

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
