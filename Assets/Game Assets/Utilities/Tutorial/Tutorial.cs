using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using Kyru.UI;
using Kyru.etc;

public class Tutorial : MonoBehaviour
{
	// Public
	public Animation square;
	public ParticleSystem fog;
	public Color targetAmbient;
	public Vector3 squarePos;

	// private
	bool started;
	Text tutoText;
	bool daveEntered;

	public void OnTriggerEnter ( Collider col )
	{
		if ( col.tag != "Player" ) return;

		if ( !started )
		{
			started = true;
			tutoText = GameObject.Find ( "TXT_Tuto" ).GetComponent<Text> ();
			StartCoroutine ( "Tutorial_Line" );
		}
		else daveEntered =  true;
	}

	IEnumerator Tutorial_Line ()
	{
		ProceduralMaterial.substanceProcessorUsage = ProceduralProcessorUsage.All;

		// Start turning down ambient
		StartCoroutine ( this.AsyncLerp ( typeof ( RenderSettings ), "ambientLight", targetAmbient, 4f ) );
		yield return new WaitForSeconds ( 3f );

		// Teleport to square
		transform.localPosition = squarePos;
		yield return new WaitUntil ( () => daveEntered );
		// => collider on
		// => start fog wall fbx
		// => modify light / ambient
		// => maybe wait a bit

		// Wait a second, then:
		// Sword tuto
		Game.dave.canCombat = true;
		yield return NewTuto ( AllTexts.R_To_Unsheathe, Key.Sword );
		square.Play ( "Fade" );
		fog.Play ();
		yield return new WaitForSeconds ( 2f );
		square.Play ( "Loop" );
	}

	IEnumerator NewTuto ( AllTexts txt, Key key ) 
	{
		Game.ui.SetTrigger ( "NewTuto" );
		tutoText.text = Localization.GetText ( txt );
		yield return new WaitUntil ( () => Game.input.GetKeyDown ( key ) );
		Game.ui.SetTrigger ( "TutoOver" );
	}
}
