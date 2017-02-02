using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParticleSpinTest : MonoBehaviour
{
	public float speed;

	void LateUpdate ()
	{
		transform.Rotate ( Vector3.forward, speed * Time.deltaTime );
	}
}
