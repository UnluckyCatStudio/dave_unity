using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
	public float speed;
	public bool cosaQueNoGiraAsi;

	public void Update () 
	{
		if (cosaQueNoGiraAsi)
		{
			GetComponent<Animation> ().Play ();
		}
		else transform.Rotate ( Vector3.forward, speed * Time.deltaTime );
	}
}