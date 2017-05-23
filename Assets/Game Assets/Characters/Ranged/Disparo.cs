using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
	private void Update ()
	{
		transform.Translate ( Vector3.forward * 6 * Time.deltaTime );
	}

	private void OnTriggerEnter ( Collider other )
	{
		if (other.tag == "Player")
		{
			other.GetComponent<DaveController> ().Hit ( transform.position );
			GetComponent<ParticleSystem> ().Stop ( true, ParticleSystemStopBehavior.StopEmitting );
			GetComponent<Collider> ().enabled = false;
			Destroy ( gameObject, 2.5f );
		}
	}
}
