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

//	[Header ("Clamp Y rotation")]
//	public float	min;
//	public float	max;

	private void LateUpdate ()
	{
		// Get user input
		var rotationY = speedY * Input.GetAxis ( "Mouse X" ) * Time.deltaTime;
		var rotationX = speedX * Input.GetAxis ( "Mouse Y" ) * Time.deltaTime;

		// Rotate
		if ( rotationY != 0 )
			transform.Rotate ( Vector3.up, rotationY );     // Controller gets only Y-axis rotation

		if ( rotationX != 0 )
			RotatePivot ( rotationX );                                 // Pivot gets *clamped* X-axis rotation

		// Look at pivot
		transform.LookAt ( pivot );

		transform.position = dave.position + Vector3.up;
	}

	#region FX
	private void RotatePivot ( float rotX ) 
	{
		pivot.Rotate ( Vector3.right, rotX );
		var rot = pivot.localEulerAngles;
		rot.z = 0;
		pivot.localRotation = Quaternion.Euler ( rot );
	}
	#endregion
}
