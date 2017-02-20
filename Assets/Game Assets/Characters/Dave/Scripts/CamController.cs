using System.Collections;
using System.Collections.Generic;
using Kyru.etc;
using UnityEngine;

public class CamController : MonoBehaviour
{
	public float speedX;
	public float speedY;

	private Vector3 rotationY = Vector3.zero;
	private Vector3 rotationX = Vector3.zero;
	private void LateUpdate ()
	{
		// Get user input
		rotationY += Vector3.up		* Input.GetAxis ( "Mouse X" ) * Time.deltaTime;
		rotationX += Vector3.right	* Input.GetAxis ( "Mouse Y" ) * Time.deltaTime;

		// Clamp rotation
		//rotationX.Clamp ( new Vector3 (  )
	}
}
