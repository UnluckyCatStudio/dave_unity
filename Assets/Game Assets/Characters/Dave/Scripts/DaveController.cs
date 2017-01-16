using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveController : MonoBehaviour
{
	private CharacterController controller;
	private Animator animator;

	// Movement
	[Header("Movement")]
	public float speed;             // Movement speed multiplier
	private float mov;				// Real movement speed
	private bool backwards;			// Is Dave moving backwards?

	// Spline tracker
	[Header("Spline tracker")]
	public RailManager rail;     // Movement spline
	public RailManager cameraRail;

	private void Update ()
	{
		// Movement + spline tracking
		if ( Input.GetKey ( KeyCode.D ) )
		{
			animator.SetBool ( "walking", true );
		}
		else animator.SetBool ( "walking", false );

	}

	private void Awake ()
	{
		controller = GetComponent<CharacterController> ();
		animator = GetComponent<Animator> ();
	}
}
