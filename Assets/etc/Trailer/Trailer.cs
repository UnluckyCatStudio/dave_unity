using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : MonoBehaviour
{
	private Animator animator;

	private void Update ()
	{
		if (Input.GetKeyDown ( KeyCode.Space ))
			animator.SetTrigger ( "keep" );
	}

	private void Start ()
	{
		animator =  GetComponent<Animator> ();
	}
}
