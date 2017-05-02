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
				if ( allCaps )  return Localization.GetText ( key ).ToUpper ();
				else			return Localization.GetText ( key );
			}
		}

		/// <summary>
		/// Updates the control text
		/// to current translation.
		/// </summary>
		public virtual void UpdateText ()
		{
			control.text = value;
		}

		public virtual void Init () 
		{
			control = GetComponent<Text> ();
		}
	} 
}
