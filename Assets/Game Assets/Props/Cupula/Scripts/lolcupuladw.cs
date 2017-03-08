using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lolcupuladw : MonoBehaviour
{
	public Animation a;
	bool f;

	void OnTriggerEnter ( Collider other )
	{
		if ( other.tag == "Player" )
		{
			a.Play ();
			RenderSettings.ambientIntensity = 0;
			RenderSettings.reflectionIntensity = 0;
		}
	}
}
