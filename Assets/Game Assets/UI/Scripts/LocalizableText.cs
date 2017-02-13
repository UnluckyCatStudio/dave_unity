using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kyru.UI;

/// <summary>
/// Class for any UI Text
/// that should be localizable.
/// </summary>
public class LocalizableText : MonoBehaviour
{
	Text   control;

	[SerializeField] bool allCaps;
	[SerializeField] string _key;

	string key   
	{
		get { return _key; }
		set
		{
			_key = value;
			UpdateText ();
		}
	}
	string value 
	{
		get
		{
			if ( allCaps )
				return Localization.texts[key].ToUpper ();
			else
				return Localization.texts[key];
		}
	}

	/// <summary>
	/// Updates the control text
	/// to current translation.
	/// </summary>
	public void UpdateText () 
	{
		control.text = value;
	}

	/// <summary>
	/// Registers this Localizable text
	/// in the Localization list so it can be updated.
	/// </summary>
	public void Register () 
	{
		Localization.registry.Add ( this );
	}

	/// <summary>
	/// Returns the localizable text dictionary entry
	/// in the format "key:value"
	/// </summary>
	public string GetKey () 
	{
		return key + ":";
	}

	void Awake ()
	{
		control = GetComponent<Text> ();
	}
}
