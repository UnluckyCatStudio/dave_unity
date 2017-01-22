using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script should be attached to
/// any UI.Text that must be translated.
/// </summary>
public class Localize : MonoBehaviour
{
	public string key;      // Text ID
	public bool allCaps;	// Is text all in caps?

	public void UpdateTranslation ()
	{
		GetComponent<Text> ().text = Localizator.GetText ( key, allCaps );
	}
}
