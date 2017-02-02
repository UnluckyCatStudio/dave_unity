using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotkeyController : MonoBehaviour
{
	// The text ( hotkey ) that is
	// currently waiting user input.
	private Text selected;

	// The text ( 'esc to cancel' ) to show
	// while waiting input.
	private Text info;

	// The index in the keys[] array
	// ( if wrong number is given, won't work! )
	private int index;

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
						Game.input.keys[index] = kcode;

						// Show new value
						selected.text = kcode.ToString (); 
					}

					// Stop waiting input
					selected.gameObject.SetActive ( true );
					selected = null;
					info.gameObject.SetActive ( false );
					info = null;
				}
			}
		}
	}

	#region FX
	public void WaitHotkey ( Text hotkey )
	{
		selected = hotkey;
		selected.gameObject.SetActive ( false );
	}

	public void SetText ( Text info )
	{
		this.info = info;
		info.gameObject.SetActive ( true );
	}

	public void SetIndex ( int index )
	{
		this.index = index;
	}
	#endregion
}
