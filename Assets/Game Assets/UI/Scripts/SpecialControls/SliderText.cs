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

		protected override void Awake ()
		{
			control = GetComponent<Text> ();
			slider  = GetComponentInParent<Slider> ();
		}
	} 
}
