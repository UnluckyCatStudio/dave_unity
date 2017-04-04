using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kyru.UI;

public class TutorialEvent : MonoBehaviour
{
	// Localization key
	public AllTexts key;

	public void Trigger ()
	{
		var t = GameObject.Find ( "TXT_Tuto" ).GetComponent<Text> ();
		t.text =
			Localization.translations[Localization.lang]
			.texts[( int ) key]
			.Replace ( "$$", "\n" )
			.Replace ( "[", "<b><color=orange>" )
			.Replace ( "]", "</color></b>" );

		Time.timeScale = 0;
		Game.ui.SetTrigger ( "Tutorial" );
		active = true;
	}

	private bool active;
	void Update ()
	{
		if ( active && Input.GetKeyDown ( KeyCode.E ) )
		{
			Game.ui.SetTrigger ( "TutorialCompleted" );
			Time.timeScale = 1;
			Destroy ( gameObject );
		}
	}

	public void OnTriggerEnter ( Collider other )
	{
		if ( other.tag == "Player" )
			Trigger ();
	}
}
