using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Tutorial : MonoBehaviour
{
	public Collider muro;
	public MeleeController first;
	public MeleeController[] firstWave;
	public Light[] lights;


	private bool waiting;
	private bool running;
	private Text text;
	private GlobalFog fog;

	public void OnTriggerEnter ( Collider col )
	{
		if ( col.tag == "Player" && !running )
		{
			// Start the tutorial
			running = true;
			text = GameObject.Find ( "TXT_Tuto" ).GetComponent<Text> ();
			Game.sun = GameObject.Find ( "SUN" ).GetComponent<Light> ();
			fog = Camera.main.GetComponent<GlobalFog> ();
			muro.enabled = true;
			StartCoroutine ( "venga" );
		}
	}

	IEnumerator venga ()
	{
		text.text = format ( "Usa [R] para desenfundar $$o enfundar la espada" );
		waiting = true;
		Game.ui.SetTrigger ( "Tutorial" );
		Time.timeScale = 0;
		Game.dave.canCombat = true;
		
		yield return new WaitUntil ( () => Input.GetKeyDown ( KeyCode.R ) );
		yield return new WaitForSeconds ( 1.8f );

		text.text = format ( "[Usa el click izquierdo] para un ataque $$rapido y directo" );
		waiting = true;
		Game.ui.SetTrigger ( "Tutorial" );
		Time.timeScale = 0;

		yield return new WaitUntil ( () => first.dead );


		var targetFogHeight = 550f;
		var targetSunIntensity = 0.5f;
		var targetLightIntensity = 1;
		var startTime = Time.time;

		while ( Time.time <= startTime + 1.2f )
		{
			fog.height = Mathf.Lerp ( fog.height, targetFogHeight, Time.deltaTime );
			Game.sun.intensity = Mathf.Lerp ( Game.sun.intensity, targetSunIntensity, Time.deltaTime * 2f );
			foreach ( var l in lights )
			{
				l.intensity = Mathf.Lerp ( l.intensity, targetLightIntensity, Time.deltaTime );
			}

			yield return null;
		}

		text.text = format ( "[Usa el click derecho] para un ataque $$mas lento de barrido" );
		waiting = true;
		Game.ui.SetTrigger ( "Tutorial" );
		Time.timeScale = 0;

		firstWave[0].gameObject.SetActive ( true );
		firstWave[0].Activate ();

		firstWave[1].gameObject.SetActive ( true );
		firstWave[1].Activate ();

		firstWave[2].gameObject.SetActive ( true );
		firstWave[2].Activate ();


		//		var duration = 3f;
		//		var start = Time.time;
		//		var timeout = Time.time + duration;
		//
		//		while ( Time.time <= timeout )
		//		{
		//			var progress = ( timeout - start ) / duration;
		//			fog.height = Mathf.Lerp (  )
		//
		//			yield return null;
		//		}
	}

	private void Update ()
	{
		if ( waiting && Input.GetKeyDown ( KeyCode.E ) )
		{
			waiting = false;
			Time.timeScale = 1;
			Game.ui.SetTrigger ( "TutorialCompleted" );
		}
	}

	private string format ( string s ) 
	{
		return s
			.Replace ( "$$", "\n" )
			.Replace ( "[", "<b><color=orange>" )
			.Replace ( "]", "</color></b>" );
	}
}
