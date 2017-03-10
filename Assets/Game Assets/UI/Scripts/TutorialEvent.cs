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
		t.text = Localization.texts[key]
			.Replace ( "$$", "\n" )
			.Replace ( "[", "<b><color=orange>")
			.Replace ( "]", "</color></b>" );

		Time.timeScale = 0;
		Game.dave.LockDave ( 1 );
		Game.ui.SetTrigger ( "Tutorial" );
		active = true;
	}

	private bool active;
	void Update ()
	{
		if ( active && Input.GetKeyDown ( KeyCode.Return ) )
		{
			Game.dave.LockDave ( 0 );
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
