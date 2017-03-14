using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Kyru.etc;

namespace Kyru.UI
{
	public static class Localization
	{
		public static Language lang = 0;
		public static Translation[] translations;

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
			if (Game.ui == null)
				Game.ui = GameObject.Find("UI").GetComponent<Animator>();

			texts.Clear ();

			return true;
		}

		public static bool InitAllTexts () 
		{
			if ( Game.ui == null )
				Game.ui = GameObject.Find ( "UI" ).GetComponent<Animator> ();

			var txts = Game.ui.GetComponentsInChildren<LocalizableText> ( true );
            registry.Clear ();
            foreach ( var t in txts )
			{
				registry.Add ( t );
				t.Init ();
			}

			return true;
		}

		public static bool UpdateAllTexts () 
		{
			foreach ( var t in registry ) t.UpdateText ();

			return true;
		}

		public static IEnumerable<string> PrintAllTexts () 
		{
			var list = new List<string> ();

			#if UNITY_EDITOR
			var txts = Game.ui.GetComponentsInChildren<LocalizableText> ( true );
			foreach ( var t in txts )
			{
				if ( !(t is SliderText) )
				{
					var k = t.GetKey ();
					if ( !list.Contains ( k ) )
					{
						yield return k;
						list.Add ( k );
					}
				}
			}
			#else
			foreach ( var t in texts )
			{
				if ( !(t is SliderText) )
				{
					if ( !list.Contains ( t.Key ) )
					{
						yield return t.Key+":"+t.Value;
						list.Add ( t.Key );
					}
				}
			}
			#endif

			// Quality-text keys
			for ( int i=0; i!=SliderText.keys.Length; i++ )
				yield return SliderText.keys[i] + ":";
		}
	}
}