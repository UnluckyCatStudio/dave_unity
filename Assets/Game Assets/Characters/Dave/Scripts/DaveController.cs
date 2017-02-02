using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveController : MonoBehaviour
{
	private Animator anim;
	private Rigidbody body;
	private Transform cam;

	// Movement
	[Header("Basic movement")]
	public	bool    canMove;		// Can the player move?
	public float	speed;			// Movement speed multiplier

	[Header("IK")]
	public bool			activeIK;       // Is Dave IK working?
	public float		weight;         // IK weight
	public Transform    leftFoot;
	public Transform    rightFoot;

	void FixedUpdate ()
	{
		if ( canMove )
		{
			#region KEYBOARD
			var inputDir = Vector3.zero;

			if ( Input.GetKey ( Game.input.keys[0] ) ) inputDir += Vector3.forward;
			if ( Input.GetKey ( Game.input.keys[1] ) ) inputDir += Vector3.back;
			if ( Input.GetKey ( Game.input.keys[2] ) ) inputDir += Vector3.left;
			if ( Input.GetKey ( Game.input.keys[3] ) ) inputDir += Vector3.right;

			// Get the actual movement direction ( relative to camera )
			var moveDir = cam.TransformDirection ( inputDir.normalized );
			// Rotate towards movement direction
			body.MoveRotation ( Quaternion.LookRotation ( moveDir ) );
			body.MovePosition ( transform.position + moveDir * Time.fixedDeltaTime );

			anim.SetFloat ( "speed", inputDir == Vector3.zero ? 0 : 1 );
			#endregion
		}
	}

	private void OnAnimatorIK ()
	{
		if ( activeIK )
		{

		}
	} 

	void Awake ()
	{
		anim	= GetComponent<Animator> ();
		body	= GetComponent<Rigidbody> ();
	}
}
