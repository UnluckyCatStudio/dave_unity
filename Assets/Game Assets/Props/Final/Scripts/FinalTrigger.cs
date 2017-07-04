using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrigger : MonoBehaviour
{
	public RangedController[] rangeds;
	public MeleeController[] melees;

	public FinalTrigger activate;
	public FinalTrigger destroy;

	private void OnTriggerEnter ( Collider other )
	{
		if ( other.tag == "Player" )
		{
			foreach (var r in rangeds) r.Activate ();
			foreach (var m in melees ) m.Activate ();
			Destroy ( gameObject );

			if (activate)
			{
				foreach (var m in activate.melees)
					m.gameObject.SetActive (true);
			}
			if (destroy)
			{
				foreach (var m in destroy.melees)
					Destroy (m.gameObject);
			}
		}
	}
}
