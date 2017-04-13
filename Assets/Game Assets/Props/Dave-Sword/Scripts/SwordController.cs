using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : Kyru.etc.AnimatorController
{
	public Collider edge;

	public void Fade ( bool fadeIn )
	{
		if ( fadeIn ) anim.SetTrigger ( "Fade-in" );
		else		  anim.SetTrigger ( "Fade-out" );
	}
}
