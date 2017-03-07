using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kyru.UI;

public class TutorialEvent : MonoBehaviour
{
	// Localization key
	public string key;

	public void Trigger ()
	{
		var t = GameObject.Find ( "TXT_Tuto" ).GetComponent<Text> ();
		t.text = Localization.texts[key];

		Time.timeScale = 0;
		Game.ui.SetTrigger ( "Tutorial" );
		Destroy ( this );
	}
}
