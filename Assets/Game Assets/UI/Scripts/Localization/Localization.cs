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
<<<<<<< HEAD
		public static int lang = 0;

=======
>>>>>>> refs/remotes/origin/menus
		/// <summary>
		/// 0 = EN
		/// 1 = ES
		/// 2 = CAT
		/// </summary>
		public static int lang = 0;
		public static Translation[] translations = new Translation[3];

		/// <summary>
		/// All the UI Text elements registered to have
		/// its translation updated.
		/// </summary>
		public static List<LocalizableText> registry = new List<LocalizableText> ();

		public static bool InitAllTexts () 
		{
			if ( Game.ui == null )
				Game.ui = GameObject.Find ( "UI" ).GetComponent<Animator> ();

            registry.Clear ();
			var txts = Game.ui.GetComponentsInChildren<LocalizableText> ( true );
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
	}
}