using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
	public float	delay;

	// When game first starts,
	// wait like X seconds
	// and then show Main Menu animation.
	private IEnumerator MainMenuAnimationDelay () 
	{
		yield return new WaitForSeconds ( delay );
		GetComponent<Animator> ().SetBool ( "OnMainMenu", true );
	}

	private void Start () 
	{
		StartCoroutine ( "MainMenuAnimationDelay" );
	}
}
