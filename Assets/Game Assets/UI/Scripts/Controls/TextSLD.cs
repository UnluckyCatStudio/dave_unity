using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Kyru.UI
{
	public class TextSLD : UI<Slider>
	{
		Slider control { get; set; }

		[Header("Texts")]
		public LocalizableText caption;
		public LocalizableText info;

		protected override void UpdateAll () 
		{
			caption.Update ();
			info.Update ();
		}

		protected override void RegisterAll () 
		{
			caption.Register ();
			info.Register ();
		}
	}
}
