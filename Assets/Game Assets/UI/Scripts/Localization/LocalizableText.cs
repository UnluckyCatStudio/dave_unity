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
		control.text = value
			.Replace ( "$$", "\n" )
			.Replace ( "[", "<b><color=orange>" )
			.Replace ( "]", "</color></b>" );
	}

	/// <summary>
	/// Returns the localizable text dictionary entry
	/// in the format "key:value"
	/// </summary>
	public string GetKey () 
	{
		return key + ":";
	}

	public virtual void Init() 
	{
		control = GetComponent<Text> ();
	}
}
