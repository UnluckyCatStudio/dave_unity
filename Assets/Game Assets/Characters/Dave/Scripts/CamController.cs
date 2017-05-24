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
	public LayerMask dontCollideWith;
	public float minDistanceFromPivot;
	public float maxDistanceFromPivot;
	[Range(0.01f,1)] public float avoidingStep;
	public float speedX;
	public float speedY;

	[Header ("Clamp Y rotation")]
	public float min;
	public float max;

	private Vector3 _lookOffset = Vector3.up;
	public Vector3 lookOffset
	{
		get { return _lookOffset; }
		set { _lookOffset = value; }
	}


	private void LateUpdate () 
	{
		// Get user input
		var rotationX = speedX * Input.GetAxis ( "Mouse Y" ) * Time.deltaTime * ( Game.input.speedY / 100f );
		var rotationY = speedY * Input.GetAxis ( "Mouse X" ) * Time.deltaTime * ( Game.input.speedX / 100f );
		// Axis inversion
		rotationX *= Game.input.invertX ? -1 : 1;
		rotationY *= Game.input.invertY ? -1 : 1;

		// Transformations
		FollowDave ();
		RotateCamera ( rotationX, rotationY );
		Stabilize ();
	}

	#region FX
	Vector3 pivot = new Vector3 ( 0, 0, -2.9f );
	public void FollowDave () 
	{
		transform.position = dave.position;						// Follow Dave
		pivot = new Vector3 ( lookOffset.x, lookOffset.y, pivot.z );
		Game.cam.transform.localPosition = pivot;
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
		if ( !TooFar () && !IsColliding ( 0.3f ) )
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

	// Charging
	public void CamCharging (bool from=false)
	{
		if ( from )
		{
			StartCoroutine ( this.AsyncLerp<CamController> ( "lookOffset", dave.up, 0.5f, this ) );
		}
		else
		{
			var look = dave.InverseTransformPoint ( pivotY.TransformPoint ( new Vector3 ( 0.85f, 1.2f, -1f ) ) );
			StartCoroutine ( this.AsyncLerp<CamController> ( "lookOffset", look, 0.25f, this ) );
		}
	}
	#endregion

	#region HELPERS
	// Is camera too close/far ?
	private bool TooClose () 
	{
		return
			-Game.cam.transform.localPosition.z
			<= minDistanceFromPivot;
	}
	private bool TooFar () 
	{
		return
			-Game.cam.transform.localPosition.z
			>= maxDistanceFromPivot;
	}

	// Performs collisions checks from camera
	private bool IsColliding ( float radius=0.15f ) 
	{
		return
			Physics.CheckSphere ( Game.cam.transform.position, radius, ~dontCollideWith );
	}
	#endregion
}
