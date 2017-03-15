using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kyru.UI
{
	public class SliderText : LocalizableText
	{
		Slider slider;
		public static AllTexts[] keys =
		{
			AllTexts.Low,
			AllTexts.Medium,
			AllTexts.High,
			AllTexts.High_plus
		};


		public override void UpdateText ()
		{
			control.text =
				Localization.translations[( int ) Localization.lang]
				.texts[( int ) keys[( int ) slider.value]];
		}

		public override void Init ()
		{
			control = GetComponent<Text> ();
			slider  = GetComponentsInParent<Slider> ( true )[0];
		}
	} 
}
