using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedController : MonoBehaviour
{
	[Header ("PARTICLE SYSTEMS")]
	public ParticleSystem ranged;
	public ParticleSystem death;
	public ParticleSystem shoot;
	public ParticleSystem decoration;
	public ParticleSystem charge;

	public float attackSpeed;
	IEnumerator Logic ()
	{
		while ( true )
		{
			RaycastHit hit =  new RaycastHit ();
			var dir = transform.position - Game.dave.transform.position;
			if
			(  Physics.Raycast ( transform.position, -dir, out hit )
			&& hit.transform.tag == "Player" )
			{
				print ( "GLA" );
			}

			yield return null;
		}
	}

	public bool active;
	private void Update () { if (active) Activate (); }

	public void Activate ()
	{
		ranged.Play ();
		decoration.Play ();
		active = true;
		StartCoroutine ( Logic () );
	}

	private void OnTriggerEnter ( Collider other )
	{
		if (other.tag == "Shot")
		{
			// Die
			ranged.Stop ( true, ParticleSystemStopBehavior.StopEmitting );
			decoration.Stop ( true, ParticleSystemStopBehavior.StopEmitting );
			charge.Stop ( true, ParticleSystemStopBehavior.StopEmitting );
			death.Play ();
			GetComponent<Collider> ().enabled = false;
			active = false;
		}
	}

	public bool startOnAwake;
	void Start () 
	{
		if (startOnAwake) Activate ();
	}
}
