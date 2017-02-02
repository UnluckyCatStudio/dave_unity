using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveCameraController : MonoBehaviour
{
	// Dave transform
	public Transform dave;

	// Own rigidbody component
	public Rigidbody body;

	// Camera look target
	public Transform look;

	// Camera min distance from look target ( local z-axis )
	private float	minDistnace		= 2f;
	private float	transition		= .7f;
	private bool    colliding       = false;

	// Camera roation speed
	private float speed = 120f;

	void FixedUpdate ()
	{
		if ( PauseManager.paused ) return;

		#region ROTATION
		var inputH = Input.GetAxis ( "Mouse X" );
		transform.RotateAround
			(
			look.position,
			transform.parent.up,
			inputH * speed * Time.fixedDeltaTime
			);

		var inputV = Input.GetAxis ( "Mouse Y" );
		transform.RotateAround
			(
			look.position,
			transform.parent.right,
			inputV * speed * Time.fixedDeltaTime
			);

		transform.LookAt ( look );
		var lookDir = ( look.position - transform.position );
		var rotation = Quaternion.LookRotation ( lookDir );
		body.MoveRotation ( rotation );
		#endregion

		#region POSITION
		// Follow Dave moment
		look.position =
			new Vector3
			(
				dave.position.x,
				dave.position.y + 1f,		// height offset
				dave.position.z
			);

		var distance = Vector3.Distance ( transform.position, look.position );
		if ( !colliding && distance < minDistnace )
		{
			var dir = ( transform.position - look.position ).normalized;
			var pos = transform.position + dir * transition * Time.fixedDeltaTime;

			body.MovePosition ( pos );
		}
		#endregion

		#region DEBUG
		var dire = ( transform.position - look.position ).normalized;
		var end = look.TransformPoint ( dire * minDistnace );
		Debug.DrawLine ( look.position, end );
		#endregion
	}

	#region COLLISION CHECK
	void OnCollisionEnter ( )
	{
		colliding = true;
	}
	void OnCollisionExit ()
	{
		colliding = false;
	}
	#endregion
}
