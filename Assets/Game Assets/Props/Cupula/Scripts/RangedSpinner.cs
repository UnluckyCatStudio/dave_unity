using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSpinner : MonoBehaviour
{
	public float speed;

	private void Update ()
	{
		transform.Rotate ( Vector3.up, speed * Time.deltaTime );
	}
}
