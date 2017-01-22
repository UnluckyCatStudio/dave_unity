using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages game text translations.
/// Acts more like a dictionary.
/// 
/// Instead of being serialized and saved as a json,
/// this class is manually converted to a human-redeable
/// file text ( .lang ) and saved in the Game folder.
/// </summary>
public static class Localizator
{
	private static string[] ids =          // Transtions ID ( accessors )
	{
		#region PAUSE MENU
		"pause_menu:pause",
		"pause_menu:resume",
		"pause_menu:options",
		"pause_menu:quit-to-menu",
		"pause_menu:quit-to-desktop",
		"pause_menu:graphics",
		"pause_menu:audio",
		"pause_menu:controls",
		"pause_menu:apply",
		"pause_menu:back",

		#region GRAPHICS
		"pause_menu:resolution",
		"pause_menu:vsync",
		"pause_menu:textures",
		"pause_menu:postfx",
		"pause_menu:shadows",
		"pause_menu:fov",
		"pause_menu:antialiasing",
		"pause_menu:low",
		"pause_menu:medium",
		"pause_menu:high",
		"pause_menu:veryhigh",
		#endregion

		#region AUDIO
		"pause_menu:master",
		"pause_menu:music",
		"pause_menu:sfx",
		"pause_menu:ambient",
		"pause_menu:voices",
		#endregion

		#region CONTROLS
		"pause_menu:forward",
		"pause_menu:backwards",
		"pause_menu:left",
		"pause_menu:right",
		"pause_menu:jump",
		"pause_menu:run",
		"pause_menu:sword",
		"pause_menu:boomerang",
		"pause_menu:lock-enemy",
		"pause_menu:attack",
		"pause_menu:interact"
		#endregion

		#endregion
	};
	private static string[] texts =        // Translations - default values
	{
		#region PAUSE MENU
		"Pause",
		"Resume",
		"Options",
		"Quit to menu",
		"Quit to desktop",
		"Graphics",
		"Audio",
		"Controls",
		"Apply",
		"Back",

		#region GRAPHICS
		"Resolution",
		"V-Sync",
		"Textures",
		"Post Fx",
		"Shadows",
		"FoV",
		"Antialiasing",
		"Low",
		"Medium",
		"High",
		"Very high",
		#endregion

		#region AUDIO
		"Master",
		"Music",
		"Effects",
		"Environment",
		"Voices",
		#endregion

		#region CONTROLS
		"Move forward",
		"Move backwards",
		"Move left",
		"Move right",
		"Jump",
		"Run",
		"Use sword",
		"Use boomerang",
		"Lock enemy",
		"Attack",
		"Interact"
		#endregion

		#endregion
	};

	private static int i;					// Global variable for loops
	public static string GetText ( string key, bool makeAllCaps = false )
	{
		for ( i = 0; i != ids.Length; i++ )
			if ( ids[i].Equals ( key ) )
				return makeAllCaps ? texts[i].ToUpper () : texts[i];

		throw new System.NotImplementedException ( "Can't find key!" );
	}

	/// <summary>
	/// Returns a collection of all
	/// default english texts.
	///	One per line.
	///	This should be used as a reference when translating.
	/// </summary>
	public static IEnumerable<string> PrintTexts ()
	{
		string temp = "";
		for ( i=0; i!=ids.Length; i++ )
		{
			temp += texts[i];
			yield return temp;
		}
	}
}
