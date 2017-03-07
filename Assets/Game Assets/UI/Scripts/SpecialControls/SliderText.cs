using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kyru.UI
{
	public class SliderText : LocalizableText
	{
		Slider slider;
		public static string[] keys =
		{
			"low",
			"medium",
			"high",
			"high+"
		};


		public override void UpdateText ()
		{
			control.text = Localization.texts[keys[( int ) slider.value]];
		}

		public override void Init ()
		{
			control = GetComponent<Text> ();
			slider  = GetComponentsInParent<Slider> ( true )[0];
		}
	} 
}
