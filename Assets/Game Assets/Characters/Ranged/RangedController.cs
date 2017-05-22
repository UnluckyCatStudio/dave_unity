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
	public LayerMask daveLayer;
	IEnumerator Logic ()
	{
		RaycastHit hit =  new RaycastHit ();
		while ( true )
		{
			var dir = ( Game.dave.transform.position + Vector3.up ) - transform.position;
			if
			( Physics.Raycast ( transform.position, dir.normalized, out hit )
			&& hit.transform.tag == "Player" )
			{
				print ( "HOLIS" );
			}

			yield return new WaitForSeconds ( attackSpeed );
		}
	}

	public bool active;
	private void Update () {}

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
