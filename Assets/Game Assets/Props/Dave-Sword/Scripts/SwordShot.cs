using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordShot : MonoBehaviour
{
	private void Update ()
	{
		transform.Translate ( 0, 0, -20f * Time.deltaTime, Space.Self );
	}

	private void OnTriggerEnter ( Collider other )
	{
		if ( other.tag == "Ranged" )
		{
			other.GetComponent<RangedController> ().Die ();
			Destroy ( gameObject );
		}
		else transform.Rotate ( Vector3.right, 45, Space.Self );   // reflect
	}
}
