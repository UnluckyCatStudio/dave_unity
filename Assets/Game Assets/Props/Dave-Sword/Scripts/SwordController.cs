using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : Kyru.etc.AnimatorController
{
	public GameObject edge;

	void Awake () 
	{
		anim = GetComponent<Animator> ();
	}
}
