using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneLoader
{
	public static void Init ( MonoBehaviour _caller, string _zone, float _delay ) 
	{
		zone = _zone;
		delay = _delay;
		caller = _caller;

		if ( working )  return;
		else			working = true;

		fx = SceneManager.LoadSceneAsync ( zone, LoadSceneMode.Single );
		caller.StartCoroutine ( LoadZone () );
		fx.allowSceneActivation = false;
	}

	private static AsyncOperation	fx;
	private static string           zone;
	private static float			delay;
	private static MonoBehaviour	caller;
	private static bool				working;

	private static IEnumerator LoadZone () 
	{
		while ( fx.progress < 0.89 )
		{
			Debug.Log ( fx.progress );
			yield return null;
		}

		yield return new WaitForSeconds ( delay );
		fx.allowSceneActivation = true;
		Game.ui.GetComponent<Animator> ().SetBool ( "Loading", false );

		working = false;
	}

}
