using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monolito : MonoBehaviour
{
	public GameObject forceWall;

	public void Trigger ( Vector3 p )
	{
		GetComponent<Rigidbody> ().isKinematic = false;
		var f = (Quaternion.Euler ( 0, -45, 0 ) * (transform.position - p).normalized) * 10f;
		GetComponent<Rigidbody> ().AddForce ( f, ForceMode.VelocityChange );
		forceWall.GetComponent<Collider> ().enabled = false;
		forceWall.GetComponent<ParticleSystem> ().Stop ( true, ParticleSystemStopBehavior.StopEmitting );
		GetComponentInChildren<ParticleSystem> ().Stop ( true, ParticleSystemStopBehavior.StopEmitting );
	}
}
