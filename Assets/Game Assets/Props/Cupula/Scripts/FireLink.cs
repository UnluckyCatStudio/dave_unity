using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLink : MonoBehaviour
{
	public float intensity;
	public bool update;
	public Light[] luses;
	public ParticleSystem[] fires;
	public ParticleSystem[] smokes;

	public void LateUpdate ()
	{
		if (update)
		{
			foreach (var l in luses)
			{
				l.intensity = intensity;
			}
		}
	}

	public void FireOn () { foreach (var f in fires) f.Play (); }
	public void SmokeOn () 
	{
		foreach (var s in smokes) s.Play ();
		foreach (var f in fires) f.Stop ( true, ParticleSystemStopBehavior.StopEmitting );
	}
}
