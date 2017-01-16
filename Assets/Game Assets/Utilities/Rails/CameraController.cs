using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The camera follows the given BezierSpline,
/// in all its axis, but only its position.
/// Always looking at the player.
/// </summary>
public class CameraController : MonoBehaviour
{
	//public BezierSpline spline;
	public Transform lookAt;

	// Enables debugging
	//public bool debug;

	private void Update ()
	{
		//transform.position = spline.GetPoint ();
		transform.LookAt ( lookAt.position );
	}
}
