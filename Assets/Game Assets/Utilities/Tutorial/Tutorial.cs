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
	public Collider placeta;
	public Animation square;
	public ParticleSystem fog;
	public Color targetAmbient;
	public Vector3 squarePos;
	public GameObject[] firstWave;
	public RangedController firstRanged;
	public GameObject[] secondWave;

	// private
	Text tutoText;

	IEnumerator StartTuto ( Collider col ) 
	{
		col.enabled = false;

		// Turn down ambient
		StartCoroutine ( this.AsyncLerp ( typeof ( RenderSettings ), "ambientLight", targetAmbient, 4f ) );
		yield return null;
	}

	IEnumerator ClosePlaceta ( Collider col ) 
	{
		col.enabled = false;

		// Close placeta
		placeta.enabled = true;

		// Sword tuto
		Game.dave.canCombat = true;
		Game.dave.canMove =  false;
		Game.dave.Moving = false;
		yield return NewTuto ( AllTexts.Tuto_R_To_Unsheathe, Key.Sword );

		// Close 
		square.Play ( "Fade" );
		yield return new WaitForSeconds ( 2f );
		fog.Play ();

		// Return Dave control
		Game.dave.canMove = true;
		StartCoroutine ( NewTuto ( AllTexts.Tuto_Click_To_Attack, Key.Attack_single ) );

		yield return new WaitForSeconds ( 4f );
		square.Play ( "Loop" );
	}

	IEnumerator KilledFirst ()
	{
		yield return new WaitForSeconds ( 1f );
		foreach (var m in firstWave) m.SetActive ( true );
		yield return new WaitForSeconds ( 2f );
		StartCoroutine(  NewTuto ( AllTexts.Tuto_Click_To_Attack_Big, Key.Attack_big ) );
	}

	int firstWaveCount;
	IEnumerator FirstWave ()
	{
		if ( ++firstWaveCount == 3 )
		{
			yield return new WaitForSeconds ( 1f );
			Game.dave.canShoot = true;
			yield return NewTuto ( AllTexts.Tuto_Charge_And_Shot, Key.Charge );
			yield return new WaitForSeconds ( 1f );
			//firstRanged
		}
	}

	IEnumerator NewTuto ( AllTexts txt, Key key ) 
	{
		Game.ui.SetTrigger ( "NewTuto" );
		tutoText.text = Localization.GetText ( txt );
		yield return new WaitUntil
			( () =>
			{
				if ( key == Key.Attack_big || key == Key.Attack_single )
				{
					return
						Game.input.GetKeyDown
						( Key.Attack_big ) || Game.input.GetKeyDown ( Key.Attack_single );
				}
				else
				if ( key == Key.Charge )
				{
					return
						Game.input.GetKeyDown
						( Key.Attack_single ) && Game.dave.Charging;
				}
				else return Game.input.GetKeyDown ( key );
			});
		Game.ui.SetTrigger ( "TutoOver" );
	}

	void Start () 
	{
		ProceduralMaterial.substanceProcessorUsage = ProceduralProcessorUsage.All;
		tutoText = GameObject.Find ( "TXT_Tuto" ).GetComponent<Text> ();
	}
}
