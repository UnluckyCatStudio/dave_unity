using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedController : MonoBehaviour
{
	[Header ("PARTICLE SYSTEMS")]
	public GameObject shot;
	public ParticleSystem ranged;
	public ParticleSystem death;
	public ParticleSystem decoration;
	public ParticleSystem charge;

	public float attackSpeed;
	public LayerMask dontCollideWith;
	IEnumerator Logic ()
	{
		RaycastHit hit =  new RaycastHit ();
		while ( true )
		{
			var dir = ( ( Game.dave.transform.position + Vector3.up ) - transform.position ).normalized;
			if
			( Physics.Raycast ( transform.position, dir, out hit, 20f, ~dontCollideWith )
			&& hit.transform.tag == "Player" )
			{
				charge.Play ();
				yield return new WaitForSeconds ( 0.15f ); // Shot delay
				var s = Instantiate ( shot, transform.position - Vector3.up, Quaternion.LookRotation (dir) );
			}

			yield return new WaitForSeconds ( 1 / attackSpeed );
			charge.Stop ( true );
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
