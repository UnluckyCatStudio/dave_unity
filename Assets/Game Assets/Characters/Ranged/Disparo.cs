using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
	float speed;
	void SetSpeed ( float s ) { speed = s; }

	private void Update ()
	{
		transform.Translate ( Vector3.forward * speed * Time.deltaTime );
	}

	private void OnTriggerEnter ( Collider other )
	{
		if (other.tag == "Player") other.GetComponent<DaveController> ().Hit ( transform.position );

		GetComponent<ParticleSystem> ().Stop ( true, ParticleSystemStopBehavior.StopEmitting );
		GetComponent<Collider> ().enabled = false;
		Destroy ( gameObject, 1.15f );
	}
}
