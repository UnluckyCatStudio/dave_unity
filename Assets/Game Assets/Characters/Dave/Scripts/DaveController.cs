using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveController : MonoBehaviour
{
	private Animator anim;
	private CharacterController me;

	// Movement
	[Header("Basic movement")]
	public bool			canMove;		// Can the player move?
	public Transform    cam;			// The camera pivot ( for relative movement )
	public float		speed;          // Movement speed multiplier

	[Header("Combat")]
	public bool canCombat;
	public bool swordOut;		

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
						diff / Time.deltaTime * .0001f   // bigger diff = faster rotation
					);
				#endregion

				me.SimpleMove ( movement * speed );
				anim.SetBool ( "Moving", true );
			}
			else anim.SetBool ( "Moving", false );
			#endregion
		}
		#endregion

		#region COMBAT
		if ( canCombat )
		{
			// Unsheathe sword
			/* 
			 * 'swordOut' is set true/false by the
			 * animation itself. Preventing issues
			 * if animation is cancelled.
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
}
