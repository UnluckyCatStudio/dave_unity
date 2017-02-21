using System;
using System.Collections;
using System.Collections.Generic;
using Kyru.etc;
using UnityEngine;

public class CamController : MonoBehaviour
{
	public	Transform   pivot;          // Pivot for rotating camera around
	public  Transform   dave;
	public	float		speedX;
	public	float		speedY;

	[Header ("Clamp Y rotation")]
	public float	min;
	public float	max;

	private void LateUpdate () 
	{
		// Get user input
		var rotationY = speedY * Input.GetAxis ( "Mouse X" ) * Time.deltaTime;
		var rotationX = speedX * Input.GetAxis ( "Mouse Y" ) * Time.deltaTime;
		// Look at pivot
		transform.LookAt ( pivot );

		// Store current rotations
		var tempX = pivot.localRotation;
		var tempY = transform.localRotation;

		// Apply rotations
		if ( rotationY != 0 )
			transform.Rotate ( Vector3.up, rotationY );     // Controller gets only Y-axis rotation

		if ( rotationX != 0 )
			ClampRotatePivot ( rotationX );                 // Pivot gets clamped X-axis rotation

		// Check for collisionss
		CheckCollisions ();

		// Follow Dave
		transform.position = dave.position + Vector3.up;
	}

	#region FX
	private void CheckCollisions ()
	{
		while ( Physics.CheckSphere ( Game.cam.transform.position, .3f ) )
		{
			if ( Vector3.Distance ( pivot.position, Game.cam.transform.position ) > 1f )
				Game.cam.transform.Translate ( 0, 0, .05f * Time.deltaTime );

			else break;
		}
	}

	private void ClampRotatePivot ( float rotX ) 
	{
		var rot	  = Quaternion.AngleAxis ( rotX, Vector3.right );
		var temp  = pivot.localRotation * rot;
		var angle = Quaternion.Angle ( Quaternion.identity, temp );

		// Clamp rotation
		if ( angle > min && angle < max ) pivot.localRotation = temp;
	}
	#endregion
}
