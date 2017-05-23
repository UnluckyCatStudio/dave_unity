using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLink : MonoBehaviour
{
	public float intensity;
	public bool update;
	public bool fireOn;
	public bool smokeOn;
	public Light[] luses;
	public ParticleSystem[] fires;
	public ParticleSystem[] smokes;

	public void Update ()
	{
		if (update)
		{
			foreach (var l in luses)
			{
				l.intensity = intensity;
			}
		}

		if (fireOn)
		{
			foreach (var f in fires) f.Play ();
			fireOn = false;
		}

		if (smokeOn)
		{
			foreach (var s in smokes)
				s.Play ();
			smokeOn = false;
		}
	}
}
