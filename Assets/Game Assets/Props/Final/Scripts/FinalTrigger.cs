using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrigger : MonoBehaviour
{
	public RangedController[] rangeds;
	public MeleeController[] melees;

	private void OnTriggerEnter ( Collider other )
	{
		if ( other.tag == "Player" )
		{
			foreach (var r in rangeds) r.Activate ();
			foreach (var m in melees ) m.Activate ();
			Destroy ( gameObject );
		}
	}
}
