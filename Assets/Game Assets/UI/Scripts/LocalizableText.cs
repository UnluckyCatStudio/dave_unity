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
	protected Text control;

	[SerializeField] private bool allCaps;
	[SerializeField] private string _key;

	private string key   
	{
		get { return _key; }
		set
		{
			_key = value;
			UpdateText ();
		}
	}
	private string value 
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
	public virtual void UpdateText () 
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

	protected virtual void Awake ()
	{
		control = GetComponent<Text> ();
	}
}
