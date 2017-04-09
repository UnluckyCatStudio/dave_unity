using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kyru.etc;

public class MeleeController : Kyru.etc.AnimatorController
{
	private CharacterController me;
	private bool on;
	public bool dead;

	public Rigidbody[] parts;

	private void Update ()
	{
		if ( !on ) return;

		transform.LookAt ( Game.dave.transform.position );
		me.Move ( Vector3.zero );

		var closeEnough = Vector3.Distance ( transform.position,  Game.dave.transform.position ) <= 2.7f;

		anim.SetBool ( "Attacking", closeEnough );
	}

	private void OnTriggerEnter ( Collider col )
	{
		if ( col.tag == "sword" )
		{
			on = false;
			anim.Stop ();
			Die ();
		}
	}

	public void Die ()
	{
		foreach ( var r in parts )
			r.isKinematic = false;

		GetComponent<CharacterController> ().enabled = false;
		dead = true;
	}

	public void Activate ()
	{
		on = true;
		me = GetComponent<CharacterController> ();
		anim.SetTrigger ( "Move" );
	}
}
