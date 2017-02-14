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
	}

}
