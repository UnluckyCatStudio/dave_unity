using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Kyru.UI
{
	public abstract class UI<T> : MonoBehaviour
	{
		T control { get; set; }

		/// <summary>
		/// Updates all the localizable texts of
		/// the UI element.
		/// </summary>
		protected abstract void UpdateAll ();

		protected abstract void RegisterAll ();

		protected void Awake () 
		{
			if ( !(control is Component) )
				throw new Exception ( "UI Type must be a component!" );

			control = GetComponent<T> ();
		}
	}

	public interface ISettings
	{
		void Discard ();
		void Load    ();
		void Save    ();
		void Apply   ();
	}

	/// <summary>
	/// Class for any UI Text
	/// that should be localizable.
	/// </summary>
	[Serializable]
	public struct LocalizableText 
	{
		[SerializeField] Text   control;
		[SerializeField] string _key;

		string key   
		{
			get { return _key; }
			set 
			{
				_key = value;
				Update ();
			}
		}
		string value 
		{
			get { return Localization.texts[key]; }
		}

		/// <summary>
		/// Updates the control text
		/// to current translation.
		/// </summary>
		public void Update () 
		{
			control.text = value;
		}

		/// <summary>
		/// Registers this Localizable text
		/// in the Localization list so it can be updated.
		/// </summary>
		public void Register () 
		{
			Localization.registry.Add ( this );
		}
	}
}
