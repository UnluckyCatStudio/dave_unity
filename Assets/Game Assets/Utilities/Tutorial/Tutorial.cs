using System.Linq;
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
	public Light sun;
	public Collider placeta;
	public Animation square;
	public ParticleSystem fog;
	public Color targetAmbient;
	public Vector3 squarePos;
	public GameObject water;
	public GameObject[] firstWave;
	public GameObject[] secondWave;
	public Color normalAmbient;
	public RangedController firstRanged;
	public Animator cupula;
	public Color cupulaAmbient;
	public GameObject beforeColliders;
	public RangedController[] firstRangeds;
	public MeleeController[] fisrtMelees;
	public RangedController[] secondRangeds;
	public MeleeController[] secondMelees;
	public RangedController[] thirdRangeds;
	public MeleeController[] lastMelees;
	public GameObject afterColliders;
	public Transform lift;
	public GameObject liftColliders;
	public Color finalAmbient;
	public Color finalFogColor;
	public Quaternion finalPivotY;
	public Quaternion finalCamRot;
	public Vector3 finalCamZoom;

	// private
	Text tutoText;

	IEnumerator StartTuto ( Collider col ) 
	{
		yield return null;
	}

	IEnumerator ClosePlaceta ( Collider col ) 
	{
		Destroy ( col.gameObject );

		// Turn down ambient
		StartCoroutine ( this.AsyncLerp<RenderSettings> ( "ambientLight", targetAmbient, 4f ) );

		// Close placeta
		placeta.enabled = true;

		// Sword tuto
		Game.dave.canCombat = true;
		Game.dave.canMove =  false;
		Game.dave.Moving = false;
		yield return NewTuto ( AllTexts.Tuto_R_To_Unsheathe, Key.Sword );

		// Close 
		square.Play ( "Fade" );
		yield return new WaitForSeconds ( 0.8f );
		fog.Play ();
		water.SetActive ( false );

		// Return Dave control
		Game.dave.canMove = true;
		StartCoroutine ( NewTuto ( AllTexts.Tuto_Click_To_Attack, Key.Attack_single ) );

		yield return new WaitForSeconds ( 6f );
		square.Play ( "Loop" );
	}

	IEnumerator KilledFirst ()
	{
		yield return new WaitForSeconds ( 1f );
		foreach (var m in firstWave) m.SetActive ( true );
		yield return new WaitForSeconds ( 0.5f );
		StartCoroutine(  NewTuto ( AllTexts.Tuto_Click_To_Attack_Big, Key.Attack_big ) );
	}

	int firstWaveCount;
	IEnumerator FirstWave () 
	{
		if ( ++firstWaveCount == firstWave.Length )
		{
			yield return new WaitForSeconds ( 2f );
			foreach (var m in secondWave) m.SetActive ( true );
		}
	}

	int secondWaveCount;
	IEnumerator SecondWave ()
	{
		if ( ++secondWaveCount == secondWave.Length )
		{
			yield return new WaitForSeconds ( 2f );
			placeta.enabled = false;
			square["Fade"].speed = -2;
			square["Fade"].time = square["Fade"].length;
			square.Play ( "Fade" );
			yield return new WaitForSeconds ( 1f );
			water.SetActive ( true );
			fog.Stop ( false, ParticleSystemStopBehavior.StopEmitting );
			StartCoroutine ( this.AsyncLerp<RenderSettings> ( "ambientLight", normalAmbient, 4f ) );
		}
	}

	IEnumerator Ranged ( Collider col ) 
	{
		col.enabled = false;
		if ( !Game.dave.SwordOut )
		{
			Game.dave.canMove = false;
			Game.dave.Moving = false;
			yield return NewTuto ( AllTexts.Tuto_R_To_Unsheathe, Key.Sword );
			yield return new WaitForSeconds ( 1f );
		}

		Game.dave.canShoot = true;
		yield return NewTuto ( AllTexts.Tuto_Charge_And_Shot, Key.Charge );
		Game.dave.canMove = true;
		firstRanged.Activate ();
	}

	IEnumerator StartCupula ( Collider col )
	{
		col.enabled = false;
		beforeColliders.SetActive ( false );

		cupula.SetTrigger ( "Close" );
		yield return new WaitForSeconds ( 3.5f );
		cupula.SetTrigger ( "FireOn" );
		StartCoroutine ( this.AsyncLerp<RenderSettings> ( "ambientLight", cupulaAmbient, 2.5f ) );
		yield return new WaitForSeconds ( 3f );
		firstRangeds[0].Activate ();
		yield return new WaitUntil ( () => !firstRangeds[0].active );
		cupula.SetBool ( "KilledRangeds_1", true );

		foreach (var r in secondRangeds) r.Activate ();
		yield return new WaitForSeconds ( 2.3f );
		foreach (var m in fisrtMelees)
		{
			m.Activate ();
			m.me.enabled = true;
		}

		yield return new WaitUntil ( () => secondRangeds.All ( x => !x.active ) );
		cupula.SetBool ( "KilledRangeds_2", true );

		foreach (var r in thirdRangeds) r.Activate ();
		yield return new WaitForSeconds ( 2.1f );
		foreach (var m in secondMelees)
		{
			m.Activate ();
			m.me.enabled = true;
		}

		yield return new WaitUntil ( () => thirdRangeds.All ( x => !x.active ) );
		cupula.SetBool ( "KilledRangeds_3", true );

		yield return new WaitForSeconds ( 2.1f );
		foreach (var m in lastMelees)
		{
			m.Activate ();
			m.me.enabled = true;
		}
	}

	int cupulaCount;
	IEnumerator OpenPlaceta () 
	{
		if ( ++cupulaCount == 8 )
		{
			afterColliders.SetActive ( true );
			cupula.SetBool ( "Stopped", true );
			yield return new WaitForSeconds ( 2.4f );
			cupula.SetBool ( "Open", true );
			StartCoroutine ( this.AsyncLerp<RenderSettings> ( "ambientLight", normalAmbient, 4f ) );
			yield return null;
		}
	}

	IEnumerator Lift ( Collider col ) 
	{
		col.enabled = false;

		liftColliders.SetActive ( true );
		Game.dave.transform.SetParent ( lift, true );
		lift.GetComponent<Animation> ().Play ();
		yield return new WaitForSeconds ( 5f );
		StartCoroutine ( this.AsyncLerp<Light> ( "intensity", 0.56f, 5f, sun ) );
		yield return new WaitForSeconds ( 2f );
		StartCoroutine ( this.AsyncLerp<RenderSettings> ( "fogColor", finalFogColor, 3f ) );
		yield return this.AsyncLerp<RenderSettings> ( "ambientLight", finalAmbient, 3f );
		//RenderSettings.skybox.SetColor ( "_Tint", skyTint );
		liftColliders.SetActive ( false );
	}

	IEnumerator Finale ( Collider col )
	{
		col.enabled = false;
		Game.dave.canMove = false;
		Game.dave.Moving = false;
		Game.dave.cam.enabled = false;

		FindObjectsOfType<MeleeController> ().ToList ().ForEach ( x => x.GetComponent<Animator> ().enabled = false );
		FindObjectsOfType<RangedController> ().ToList ().ForEach ( x => x.active = false );

		StartCoroutine ( this.AsyncLerp<Transform> ( "localRotation", finalPivotY, 8f, Game.dave.cam.transform ) );
		StartCoroutine ( this.AsyncLerp<Transform> ( "localRotation", finalCamRot, 5f, Game.cam.transform ) );
		yield return new WaitForSeconds ( 3.5f );
		StartCoroutine ( this.AsyncLerp<Transform> ( "localPosition", finalCamZoom, 6f, Game.cam.transform ) );
		yield return new WaitForSeconds ( 6f );

		GameObject.Find ( "END" ).GetComponent<Image> ().enabled = true;
		GameObject.Find ( "END" ).GetComponentInChildren<Text> ().enabled = true;

		yield return new WaitForSeconds ( 4f );
		Game.ui.GetComponent<FxUI> ().QuitToMainMenu ();

		Game.ui.GetComponentInChildren<BoxCollider> ( true ).gameObject.SetActive ( true );
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
					Game.dave.SwordOut
					&& Game.input.GetKeyDown
					( Key.Attack_big ) || Game.input.GetKeyDown ( Key.Attack_single );
			}
			else return Game.input.GetKeyDown ( key );
		});

		Game.ui.SetTrigger ( "TutoOver" );
	}

	void Start () 
	{
		//ProceduralMaterial.substanceProcessorUsage = ProceduralProcessorUsage.All;
		tutoText = GameObject.Find ( "TXT_Tuto" ).GetComponent<Text> ();
	}
}
