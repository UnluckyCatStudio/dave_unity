using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kyru.etc
{
	public static class ZoneLoader
	{
		public static void Init ( MonoBehaviour _caller, string _zone, float _delay )
		{
			zone = _zone;
			delay = _delay;
			caller = _caller;

			if ( working )  return;
			else			working = true;

			caller.StartCoroutine ( LoadZone () );
		}

		private static AsyncOperation   fx;
		private static string           zone;
		private static float            delay;
		private static MonoBehaviour    caller;
		private static bool             working;

		private static IEnumerator LoadZone ()
		{
			yield return new WaitForSeconds ( delay / 2 );
			fx = SceneManager.LoadSceneAsync ( zone, LoadSceneMode.Single );
			fx.allowSceneActivation = false;

			while ( fx.progress < 0.89 )
			{
//				Debug.Log ( fx.progress );
				yield return null;
			}

			fx.allowSceneActivation = true;
			yield return new WaitForSeconds ( delay / 2 );
			Game.ui.GetComponent<Animator> ().SetBool ( "Loading", false );

			working = false;
		}
	} 
}
