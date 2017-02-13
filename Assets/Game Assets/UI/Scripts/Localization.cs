using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Kyru.UI
{
	public static class Localization
	{
		public static int lang = 0;
		public static string[] availableLanguage =
		{
			"en",
			"es",
			"cat"
		};

		/// <summary>
		/// All the translations of the UI.
		/// Including: Menus and HUD.
		/// </summary>
		public static Dictionary<string, string> texts = new Dictionary<string, string> ();

		/// <summary>
		/// All the UI Text elements registered to have
		/// its translation updated.
		/// </summary>
		public static List<LocalizableText> registry = new List<LocalizableText> ();

		public static bool LoadTexts () 
		{
			/*
			 * BUILD:
			 * /PATH/Kyru_Data/Lang/*.lang
			 * -
			 * EDITOR:
			 * /PATH/Assets/Lang/*.lang
			*/
			var path = Application.dataPath + "/Lang/" + availableLanguage[lang] + ".lang";

			using ( var f = new FileStream ( path, FileMode.Open, FileAccess.Read ) )
			using ( var s = new StreamReader ( f ) )
			{
				// key:Value
				var line = s.ReadLine ().Split ( ':' );
				while ( line != null )
				{
					texts.Add ( line[0], line[1] );
					line = s.ReadLine ().Split ( ':' );
				}
			}

			return true;
		}

		public static bool UpdateAllTexts () 
		{
			foreach ( var t in registry ) t.Update ();

			return true;
		}

		public static IEnumerable<string> PrintAllTexts ()
		{
			#if UNITY_EDITOR
				var ui = GameObject.Find ( "UI" );
				foreach ( var t in ui.GetComponentsInChildren<LocalizableText> ( true ) )
					yield return t.GetKeyValue ();
			#else
				foreach ( var t in texts )
					yield return t.Key+":"+t.Value;
			#endif
		}
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

		/// <summary>
		/// Returns the localizable text dictionary entry
		/// in the format "key:value"
		/// </summary>
		public string GetKeyValue () 
		{
			return key+":"+value;
		}
	}
}