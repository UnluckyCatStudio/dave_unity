using System;
using System.Collections;
using System.Collections.Generic;
using Kyru.etc;
using UnityEngine;

public class CamController : MonoBehaviour
{
	[Header("References")]
	public Transform dave;
	public Transform pivot;          // Pivot for rotating camera around

	[Header("Settings")]
	public Vector3 offsetFromDave;
	public float minDistanceFromPivot;
	public float maxDistanceFromPivot;
	public float speed;
	public float speedX;
	public float speedY;

	[Header ("Clamp Y rotation")]
	public float min;
	public float max;

	private void LateUpdate () 
	{
		// Follow Dave
		transform.position = dave.position + offsetFromDave;

		// Get user input
		var rotationY = speedY * Input.GetAxis ( "Mouse X" ) * Time.deltaTime;
		var rotationX = speedX * Input.GetAxis ( "Mouse Y" ) * Time.deltaTime;
		// Axis inversion
		rotationX *= Game.input.invertX ? -1 : 1;
		rotationY *= Game.input.invertY ? -1 : 1;
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
		if ( CheckCollisions () )
		{
			// This happens when camera is colliding but still
			// is too close too pivot.
			// Rotations are reverted. 
			pivot.localRotation     = tempX;
			transform.localRotation = tempY;
		}
	}

	#region FX
	/// <summary>
	/// Makes camera go into
	/// boomerang-focus mode
	/// </summary>
	public void FocusBoomerang ()
	{

	}

	// Did Camera collide in last frame?
	/*
	 * When true, it is safe for the camera
	 * to zoom out to default position.
	 */
	bool collidedInLastFrame;
	bool isColliding 
	{
		get { return Physics.CheckSphere ( Game.cam.transform.position, .3f ); }
	}

	/// <summary>
	/// Moves the camera rig in order to avoid collisions.
	/// </summary>
	/// <returns>
	/// Returns true if Camera is colliding but it's
	/// too close to minimun distance to pivot. In that case,
	/// rotations should be reverted.
	/// </returns>
	private bool CheckCollisions ()
	{
		var collidingThisFrame = isColliding;
		// If camera isn't colliding, nor did collide
		// in the last frame, it is save to return
		// to default position.
		if ( !collidedInLastFrame && !collidingThisFrame )
		{
			if ( ( Vector3.Distance ( pivot.position, Game.cam.transform.position ) < maxDistanceFromPivot ) )
				Game.cam.transform.Translate ( 0, 0, -speed * 20f * Time.deltaTime );

			return false;
		}

		if ( collidingThisFrame )
		do
		{
			if ( Vector3.Distance ( pivot.position, Game.cam.transform.position ) > minDistanceFromPivot )
				Game.cam.transform.Translate ( 0, 0, speed * Time.deltaTime );
				
			else return true; // Camera is too close!

		} while ( isColliding );

		collidedInLastFrame = collidingThisFrame;
		return false;
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
