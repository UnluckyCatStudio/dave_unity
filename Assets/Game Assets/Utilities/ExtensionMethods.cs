using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyru.etc
{
	public static class ExtensionMethods
	{
		public static void Times ( this int max, Action action ) 
		{
			for ( int i = 0;i!=max;i++ )
				action ();
		}

		public static Vector3 Clamp ( this Vector3 v, Vector3 min, Vector3 max ) 
		{
			var r = Vector3.zero;
			for ( int i=0; i!=2; i++ )
			{
				if		( v[i] < min[i] )	r[i] = min[i];
				else if ( v[i] > max[i] )	r[i] = max[i];
				else						r[i] = v[i];
			}

			return r;
		}
	}

}
