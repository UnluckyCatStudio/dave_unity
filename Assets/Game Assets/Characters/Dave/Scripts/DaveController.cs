using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveController : MonoBehaviour
{
	private Animator anim;
	private CharacterController me;

	// Movement
	[Header("Basic movement")]
	public Transform    cam;			// The camera pivot ( for relative movement )
	public bool			canMove;		// Can the player move?
	public float		speed;          // Movement speed multiplier
	public float        rotationSpeed;

//	[Header("IK")]
//	public Transform    leftFoot;
//	public Transform    rightFoot;

	void Update ()
	{
		if ( canMove )
		{
			#region MOVEMENT
			var movement = Vector3.zero;
			// Get movement relative to camera rotation
			if ( Input.GetKey ( Game.input.keys[(int)Key.Forward] ) )	movement += cam.forward;
			if ( Input.GetKey ( Game.input.keys[(int)Key.Backwards] ) ) movement -= cam.forward;
			if ( Input.GetKey ( Game.input.keys[(int)Key.Left] ) )		movement -= cam.right;
			if ( Input.GetKey ( Game.input.keys[(int)Key.Right] ) )		movement += cam.right;
			// Only keep direction of movement
			movement.Normalize ();

			if ( movement != Vector3.zero )
			{
				me.SimpleMove ( movement * speed );
				anim.SetBool ( "Moving", true );
			}
			else anim.SetBool ( "Moving", false );
			#endregion

			#region ROTATION
			// poor implementattion
			transform.rotation = cam.rotation;
			#endregion
		}
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
