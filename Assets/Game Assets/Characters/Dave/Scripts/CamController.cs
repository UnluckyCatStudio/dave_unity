using System;
using System.Collections;
using System.Collections.Generic;
using Kyru.etc;
using UnityEngine;

public class CamController : MonoBehaviour
{
	[Header("References")]
	public Transform dave;
	public Transform pivotY;
	public Transform pivotX;

	[Header("Settings")]
	public Vector3 lookOffset;
	public float minDistanceFromPivot;
	public float maxDistanceFromPivot;
	[Range(0.01f,1)] public float avoidingStep;
	public float speedX;
	public float speedY;

	[Header ("Clamp Y rotation")]
	public float min;
	public float max;

	private void Update () 
	{
		// Get user input
		var rotationX = speedX * Input.GetAxis ( "Mouse Y" ) * Time.deltaTime;
		var rotationY = speedY * Input.GetAxis ( "Mouse X" ) * Time.deltaTime;
		// Axis inversion
		rotationX *= Game.input.invertX ? -1 : 1;
		rotationY *= Game.input.invertY ? -1 : 1;

		// Transformations
		RotateCamera ( rotationX, rotationY );
		Stabilize ();
	}

	#region FX
	public void FollowDave () 
	{
		// Follow Dave movement
		pivot = dave.position + lookOffset;
		transform.position = pivot;

		// If camera collides when moved
		while ( IsColliding () )
		{
			Game.cam.transform.Translate
				( Vector3.forward * avoidingStep );
		}
	}

	// Unchanged rotations
	Quaternion tempX;
	Quaternion tempY;
	private void RotateCamera ( float X, float Y ) 
	{
		tempX = pivotX.localRotation;
		tempY = pivotY.localRotation;

		// Clamped rotations
		if ( X != 0 )
		{
			var rot	  = Quaternion.AngleAxis ( X, Vector3.right );
			var prev  = pivotX.localRotation * rot;
			var angle = Quaternion.Angle ( Quaternion.identity, prev );

			if ( angle >= min && angle <= max )
				pivotX.localRotation = prev;
		}
		// Don't clamp Y-axis rotation
		if ( Y != 0 ) pivotY.Rotate ( Vector3.up, Y );

		// If camera collides when rotating
		while ( IsColliding () )
		{
			if ( !TooClose () )
			{
				Game.cam.transform.Translate
					( Vector3.forward * avoidingStep );
			}
			else
			{
				pivotX.localRotation = tempX;
				pivotY.localRotation = tempY;
				break;
			}
		}
	}

	private void Stabilize () 
	{
		if ( !TooFar () && !IsColliding ( avoidingStep * 1.2f ) )
		{
			var z =
			Mathf.Lerp
			(
				Game.cam.transform.localPosition.z,
				-maxDistanceFromPivot,
				Time.deltaTime
			);

			Game.cam.transform.localPosition =
			new Vector3
			(
				Game.cam.transform.localPosition.x,
				Game.cam.transform.localPosition.y,
				z
			);
		}
	}
	#endregion

	#region HELPERS
	// Is camera too close/far ?
	Vector3 pivot;
	private bool TooClose () 
	{
		return
			Vector3.Distance ( pivot, Game.cam.transform.position )
			<= minDistanceFromPivot;
	}
	private bool TooFar () 
	{
		return
			Vector3.Distance ( pivot, Game.cam.transform.position )
			>= maxDistanceFromPivot;
	}

	// Performs collisions checks from camera
	private bool IsColliding ( float radius=0.1f ) 
	{
		return
			Physics.CheckSphere ( Game.cam.transform.position, radius );
	}
	#endregion
}
