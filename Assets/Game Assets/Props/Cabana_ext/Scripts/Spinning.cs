using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
	public float speed;

	public void Update () 
	{
		transform.Rotate ( Vector3.forward, speed * Time.deltaTime );
	}
}