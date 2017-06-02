using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kyru.etc;

public class MeleeController : Kyru.etc.AnimatorController
{
	[HideInInspector]
	public CharacterController me;

	[Header("Settings")]
	public bool startOnAwake;
	public float runMul;
	public bool dead;
	public bool killAlways;

	[Header("Death")]
	public GameObject receiver;
	public string msg;

	public Rigidbody[] parts;

	private bool active;
	private void Update ()
	{
		if ( !active ) return;

		var altDavePos = Vector3.Scale (Game.dave.transform.position, new Vector3 ( 1f, 0f, 1f ) ) + Vector3.up * transform.position.y;
		transform.rotation = Quaternion.LookRotation ( altDavePos - transform.position );
		me.Move ( Vector3.zero );

		var closeEnough = Vector3.Distance ( transform.position,  Game.dave.transform.position ) <= 1f;

		anim.SetBool ( "Attacking", closeEnough );

	}

	private void FixedUpdate () 
	{
		// I have to manage collision checks by my own
		// since Unity collision table isn't by my side
		if ( anim.GetBool ( "DealingDmg" ) )
		{
			var colsR = Physics.OverlapSphere ( handR.position, 0.2f );
			var colsL = Physics.OverlapSphere ( handL.position, 0.2f );

			foreach ( var c in colsR )
			{
				if ( c.tag == "Player" )
					c.SendMessage ( "Hit", handR.position );
			}
			foreach ( var c in colsL )
			{
				if ( c.tag == "Player" )
					c.SendMessage ( "Hit", handL.position );
			}
		}
	}

	#region DYING
	private void OnTriggerEnter ( Collider col ) 
	{
		if ( ( active || killAlways ) && col.tag == "sword" )
		{
			active = false;
			anim.Stop ();
			Die ();

			if (receiver)
				receiver.GetComponent<MonoBehaviour> ()
				.StartCoroutine ( msg );
		}
	}

	public void Die () 
	{
		foreach ( var r in parts )
		{
			if (r.isKinematic) r.isKinematic = false;
			r.constraints = RigidbodyConstraints.None;
			r.AddForce ( Game.dave.transform.forward.normalized * 5f + Vector3.up * 2f, ForceMode.Impulse );
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

		anim.SetFloat ( "RunMul", runMul );
	}

	Transform handR;
	Transform handL;
	protected override void Awake () 
	{
		base.Awake ();

		handR = anim.GetBoneTransform ( HumanBodyBones.RightHand );
		handL = anim.GetBoneTransform ( HumanBodyBones.LeftHand );

		foreach ( var p in parts ) p.Sleep ();

		if (startOnAwake)
			Activate ();
	}
}
