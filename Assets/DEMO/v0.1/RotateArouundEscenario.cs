using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArouundEscenario : MonoBehaviour
{
	public Transform t;

	private void Update ()
	{
		transform.RotateAround ( t.up, .05f * Time.deltaTime );
	}
}
