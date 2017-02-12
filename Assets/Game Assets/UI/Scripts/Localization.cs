using UnityEngine;
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
	} 
}