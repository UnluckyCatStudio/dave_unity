using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedController : MonoBehaviour
{
	[Header ("PARTICLE SYSTEMS")]
	public ParticleSystem ranged;
	public ParticleSystem death;
	public ParticleSystem shoot;
	public ParticleSystem decoration;
	public ParticleSystem charge;

	private void Update ()
	{
		if ( !active ) return;


	}

	private bool active;
	public void Activate ()
	{
		ranged.Play ();
		decoration.Play ();
		active = true;
	}

	public bool startOnAwake;
	void Awake () 
	{
		if (startOnAwake) Activate ();
	}
}
