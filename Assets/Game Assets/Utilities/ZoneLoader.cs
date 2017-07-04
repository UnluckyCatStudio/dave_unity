using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Kyru.UI;

namespace Kyru.etc
{
	public static class ZoneLoader
	{
		public static void Init ( MonoBehaviour _caller, string _zone, float _delay, bool _avoidVid )
		{
			zone = _zone;
			delay = _delay;
			caller = _caller;
			avoidVid = _avoidVid;

			if ( working )  throw new Exception ( "Zone loader working" );
			else			working = true;

			caller.StartCoroutine ( LoadZone () );
		}

		private static AsyncOperation   fx;
		private static string           zone;
		private static float            delay;
		private static MonoBehaviour    caller;
		private static bool             working;
		private static bool				avoidVid;

		private static IEnumerator LoadZone ()
		{
			// 2bcontinued
			GameObject.Find ( "END" ).GetComponent<Image> ().enabled = false;
			GameObject.Find ( "END" ).GetComponentInChildren<Text> ().enabled = false;

			yield return new WaitForSeconds ( delay / 2 );
			fx = SceneManager.LoadSceneAsync ( zone, LoadSceneMode.Single );
			fx.allowSceneActivation = false;

			while ( fx.progress < 0.89 )
			{
//				Debug.Log ( fx.progress );
				yield return null;
			}

			fx.allowSceneActivation = true;

			// play vid
			if (!avoidVid)
			{
				yield return new WaitForSeconds ( delay / 2 );
				Game.ui.GetComponent<Animator> ().SetBool ( "Loading", false );
				Game.dave.cam.enabled = false;
				var m = GameObject.Find ( "Movie" );
				(m.GetComponent<RawImage> ().texture as MovieTexture).Play ();
				m.GetComponent<AudioSource> ().Play ();
				yield return new WaitForSeconds ( 60 + 16 );
				Game.ui.SetTrigger ( "MovieText" );
				yield return new WaitForSeconds ( 6f );

				Game.dave.cam.enabled = true;
				FxUI._canPause = true; 
			}
			else
			{
				Game.ui.SetTrigger ( "MovieText" );
				FxUI._canPause = true;
				yield return new WaitForSeconds ( delay * 2 );
				Game.ui.GetComponent<Animator> ().SetBool ( "Loading", false );
			}

			working = false;
		}
	} 
}
