using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyru.etc
{
	public abstract class AnimatorController : MonoBehaviour
	{
		[HideInInspector] public Animator anim;

		private void SetBool ( string param ) 
		{
			if (locked) return;

			var split = param.Split ( ':' );
			var name  = split[0];
			var value = bool.Parse ( split[1] );
			anim.SetBool ( name, value );
		}

		protected bool locked;
		protected void Unlock () { locked=false; }

		protected virtual void Awake () 
		{
			anim = GetComponent<Animator> ();
		}
	}
}