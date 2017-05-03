using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoEvent : MonoBehaviour
{
	public string eventName;

	private void OnTriggerStay ( Collider other ) 
	{
		if (other.tag == "Player")
			tutorialManager.StartCoroutine
				( eventName, GetComponent<Collider> () );
	}

	Tutorial tutorialManager;
	void Awake () 
	{
		tutorialManager = GameObject.Find ( "TUTO" ).GetComponent<Tutorial> ();
	}
}
