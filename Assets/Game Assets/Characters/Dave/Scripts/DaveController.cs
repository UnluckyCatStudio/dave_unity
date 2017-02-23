using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveController : Kyru.etc.AnimatorController
{
	private Animator anim;
	private CharacterController me;

	// Movement
	[Header("Basic movement")]
	public bool			canMove;		// Can the player move?
	public Transform    cam;			// The camera pivot ( for relative movement )
	public float		speed;          // Movement speed multiplier

	[Header("Combat")]
	public  bool canCombat;
	private bool swordOut;		
	public  SwordController sword;

//	[Header("IK")]
//	public Transform    leftFoot;
//	public Transform    rightFoot;

	void Update ()
	{
		#region BASIC MOVEMENT
		if ( canMove )
		{
			#region MOVEMENT
			var movement = Vector3.zero;
			// Get movement relative to camera rotation
			if ( Game.input.GetKey ( Key.Forward ) )	movement += cam.forward;
			if ( Game.input.GetKey ( Key.Backwards ) )  movement -= cam.forward;
			if ( Game.input.GetKey ( Key.Left ) )		movement -= cam.right;
			if ( Game.input.GetKey ( Key.Right ) )		movement += cam.right;
			// Keep only direction of movement
			movement.Normalize ();

			if ( movement != Vector3.zero )
			{
				#region CORRECT ROTATION
				var rotDir = Quaternion.LookRotation ( movement );
				var diff   = Quaternion.Angle ( transform.rotation, rotDir );

				transform.rotation =
					Quaternion.Slerp
					(
						transform.rotation,
						rotDir,
						diff / 5f * Time.deltaTime   // bigger diff = faster rotation
					);
				#endregion

				me.Move ( movement * speed * Time.deltaTime );
				anim.SetBool ( "Moving", true );
			}
			else anim.SetBool ( "Moving", false );
			#endregion
		}
		#endregion

		#region COMBAT
		if ( canCombat )
		{
			// (un)Sheathe sword
			/* 
			 * 'swordOut' is set true/false by the
			 * animation itself, preventing issues
			 * if animation is cancelled, etc
			 */
			if ( Game.input.GetKey ( Key.Sword ) )
			{
				if ( !swordOut ) anim.SetTrigger ( "Unsheathe" );
				else			 anim.SetTrigger ( "Sheathe" );
			}
		}
		#endregion
	}

//	private void OnAnimatorIK ()
//	{
//		if ( activeIK )
//		{
//
//		}
//	} 

	void Awake () 
	{
		anim	= GetComponent<Animator> ();
		me		= GetComponent<CharacterController> ();
	}

	#region EVENTS
	[Header ("Animations")]
	public Transform swordBeltHolder;
	public Transform swordHandHolder;
	/// <summary>
	/// Attaches the sword to either
	/// the belt or the hand.
	/// </summary>
	void Seathe ( int unseathe )
	{
		sword.transform.SetParent
		(
			unseathe == 0 ?
			swordHandHolder
			:
			swordBeltHolder
		);

		sword.transform.localPosition = Vector3.zero;
		sword.transform.localRotation = Quaternion.identity;
		sword.StartCoroutine ( "Fade", true );
	}
	#endregion
}
