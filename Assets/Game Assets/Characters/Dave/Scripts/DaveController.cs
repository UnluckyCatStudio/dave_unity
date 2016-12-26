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
	public BezierSpline rail;     // Movement spline
	public BezierSpline cameraRail;

	private void Update ()
	{
		// Movement + spline tracking
		if ( Input.GetKey ( KeyCode.D ) )
		{
			mov = speed * Time.deltaTime;

			rail.T += mov;
			cameraRail.T += mov;

			var pos = rail.GetPoint ();
			var dir = rail.GetDirectionPoint ();
			var rot = Quaternion.LookRotation ( dir );

			transform.rotation = rot;
			transform.position = pos;		// Remove Y-axis position

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
