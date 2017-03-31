using System;
using System.Collections;
using System.Collections.Generic;
using Kyru.etc;
using UnityEngine;

public class CamController : MonoBehaviour
{
	[Header("References")]
	public Transform dave;
	public Transform pivotLook;
	public Transform pivotY;
	public Transform pivotX;

	[Header("Settings")]
	public Vector3 offsetFromDave;
	public float minDistanceFromPivot;
	public float maxDistanceFromPivot;
	public float avoidingSpeed;
	public float speedX;
	public float speedY;

	[Header ("Clamp Y rotation")]
	public float min;
	public float max;

	private void Update () 
	{
		// Follow Dave
		transform.position = dave.position + offsetFromDave;

		// Get user input
		var rotationX = speedX * Input.GetAxis ( "Mouse Y" ) * Time.deltaTime;
		var rotationY = speedY * Input.GetAxis ( "Mouse X" ) * Time.deltaTime;
		// Axis inversion
		rotationX *= Game.input.invertX ? -1 : 1;
		rotationY *= Game.input.invertY ? -1 : 1;
		// Look at pivot
		transform.LookAt ( pivotLook );

		// Rotation and collision check
		RotateCamera ( rotationX, rotationY );
		CollisionCheck ();
	}

	#region FX
	bool isColliding 
	{
		get { return Physics.CheckSphere ( Game.cam.transform.position, .3f ); }
	}
	Quaternion tempX;
	Quaternion tempY;
	Vector3 tempLoc;

	private void RotateCamera ( float X, float Y ) 
	{
		// Clamped rotations
		if ( X != 0 )
		{
			var rot	  = Quaternion.AngleAxis ( X, Vector3.right );
			var prev  = pivotX.localRotation * rot;
			var angle = Quaternion.Angle ( Quaternion.identity, prev );

			if ( angle >= min && angle <= max ) pivotX.localRotation = prev;

		}
		// Don't clamp Y-axis rotation
		if ( Y != 0 ) pivotY.Rotate ( Vector3.up, Y );
	}
	private void CollisionCheck () 
	{
		if ( isColliding )
		{
			// Use previous values
			pivotX.localRotation = tempX;
			pivotY.localRotation = tempY;
			transform.localPosition = tempLoc;
		}
		else
		{
			// Save current values
			tempX = pivotX.localRotation;
			tempY = pivotY.localRotation;
			tempLoc = transform.localPosition;
		}
	}
	#endregion
}
