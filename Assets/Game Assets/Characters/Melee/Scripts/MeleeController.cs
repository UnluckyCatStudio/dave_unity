using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kyru.etc;

public class MeleeController : Kyru.etc.AnimatorController
{
	private CharacterController me;

	private bool active;
	public bool dead;
	public float attackDistance;

	public Rigidbody[] parts;

	private void Update ()
	{
		CheckHit ();
		if ( !active ) return;

		transform.LookAt ( Game.dave.transform.position );
		me.Move ( Vector3.zero );

		var closeEnough = Vector3.Distance ( transform.position,  Game.dave.transform.position ) <= attackDistance;

		anim.SetBool ( "Attacking", closeEnough );

	}

	private void CheckHit ()
	{
		// I have to manage collision checks by my own
		// since Unity collision table isn't by my side
		if ( anim.GetBool ( "DealingDmg" ) )
		{
			var handR = anim.GetBoneTransform ( HumanBodyBones.RightHand ).position;
			var handL = anim.GetBoneTransform ( HumanBodyBones.LeftHand ).position;

			var colsR = Physics.OverlapSphere ( handR, 0.8f );
			var colsL = Physics.OverlapSphere ( handL, 0.8f );

			foreach ( var c in colsR )
			{
				if ( c.tag == "Player" )
					c.SendMessage ( "Hit", handR );
			}
			foreach ( var c in colsL )
			{
				if ( c.tag == "Player" )
					c.SendMessage ( "Hit", handL );
			}
		}
	}

	#region DYING
	private void OnTriggerEnter ( Collider col ) 
	{
		if ( col.tag == "sword" )
		{
			active = false;
			anim.Stop ();
			Die ( Game.dave.sword.transform.position );
		}
	}

	public void Die ( Vector3 contact ) 
	{
		foreach ( var r in parts )
		{
			r.constraints = RigidbodyConstraints.None;
			r.AddExplosionForce ( 8f, contact, 1.2f, 0.4f, ForceMode.Impulse );
		}

		GetComponent<CharacterController> ().enabled = false;
		dead = true;
	}
	#endregion

	public void Activate ()
	{
		active = true;
		me = GetComponent<CharacterController> ();
		anim.SetTrigger ( "Move" );
	}
}
