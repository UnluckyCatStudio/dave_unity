using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kyru.UI
{
	/// <summary>
	/// Class for any UI Text
	/// that should be localizable.
	/// </summary>
	public class LocalizableText : MonoBehaviour
	{
		protected Text control;

		[SerializeField] private bool allCaps;
		public AllTexts key;

		private string value 
		{
			get
			{
				if ( allCaps )
					return
						Localization.translations[( int ) Localization.lang]
						.texts[( int ) key]
						.ToUpper ();
				else
					return
						Localization.translations[( int ) Localization.lang]
						.texts[( int ) key];
			}
		}

		/// <summary>
		/// Updates the control text
		/// to current translation.
		/// </summary>
		public virtual void UpdateText ()
		{
			control.text = value
				.Replace ( "$$", "\n" )
				.Replace ( "[", "<b><color=orange>" )
				.Replace ( "]", "</color></b>" );
		}

		public virtual void Init () 
		{
			control = GetComponent<Text> ();
		}
	} 
}
