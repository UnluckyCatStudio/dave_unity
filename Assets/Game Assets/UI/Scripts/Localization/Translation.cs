using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyru.UI
{
	public enum Language
	{
		EN,
		ES,
		CAT
	}

	// SHOULD ONLY HAVE ONE FOR
	// EACH LANGUAGE

	[CreateAssetMenu(order = 1000)]
	public class Translation : ScriptableObject
	{
		public Language language;
		public string[] texts;
	} 
}
