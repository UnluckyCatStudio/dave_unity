using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedController : MonoBehaviour
{
	[Header ( "Settings" )]
	public float shotSpeed;
	public float attackSpeed;
	public float range;
	public float delay;

	[Header ("PARTICLE SYSTEMS")]
	public GameObject shot;
	public ParticleSystem ranged;
	public ParticleSystem death;
	public ParticleSystem charge;

	public LayerMask dontCollideWith;
	IEnumerator Logic ()
	{
		RaycastHit hit =  new RaycastHit ();
		while ( active )
		{
			var pos = transform.position - Vector3.up * 2f;
			var dPos = Game.dave.transform.position + Vector3.up * 1.3f;
			var dir = ( dPos - pos ).normalized;
			if
			( Physics.Raycast ( pos, dir, out hit, range, ~dontCollideWith )
			&& hit.transform.tag == "Player" )
			{
				charge.Play ();
				yield return new WaitForSeconds ( delay );
				var s = Instantiate ( shot, pos, Quaternion.LookRotation (dir) );
				s.SendMessage ( "SetSpeed", shotSpeed );
				DestroyObject ( s, 10f );
			}

			yield return new WaitForSeconds ( 1 / attackSpeed );
			charge.Stop ( true );
		}
	}

	public bool active;
	private void Update () { }

	public void Activate ()
	{
		ranged.Play ();
		active = true;
		StartCoroutine ( Logic () );
	}

	public void Die ()
	{
		ranged.Stop ( true, ParticleSystemStopBehavior.StopEmitting );
		charge.Stop ( true, ParticleSystemStopBehavior.StopEmitting );
		death.Play ();
		GetComponent<Collider> ().enabled = false;
		active = false;
	}

	public bool startOnAwake;
	void Start () 
	{
		if (startOnAwake) Activate ();
	}
}
