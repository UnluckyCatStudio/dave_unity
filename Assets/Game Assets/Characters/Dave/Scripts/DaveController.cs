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
	public bool     canMove;        // Can the player move?
	public float	speed;          // Movement speed multiplier

	[Header("IK")]
	public bool			activeIK;       // Is Dave IK working?
	public float		weight;         // IK weight
	public Transform    leftFoot;
	public Transform    rightFoot;

	void FixedUpdate ()
	{
		if ( canMove )
		{
			#region MOVEMENT
			

			if ( Input.GetKey ( Game.input.keys[0] ) )
			{
				anim.SetFloat ( "walking", speed );
			}
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
