using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kyru.UI
{
	public enum IntType 
	{
		Percent,
		Angle
	}

	public class SliderInt : MonoBehaviour
	{
		private Text    text;
		public  IntType type;

		public void UpdateInfo ( float value ) 
		{
			var s = ( ( int ) value ).ToString ();

			if ( type == IntType.Angle )
				s += " º";
			else
				s += " %";

			text.text = s;
		}

		public void Init () 
		{
			text = GetComponent<Text> ();
		}
	} 
}
