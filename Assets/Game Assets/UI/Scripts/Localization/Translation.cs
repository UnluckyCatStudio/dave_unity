using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language 
{
	EN,
	ES,
	CAT
}

[CreateAssetMenu]
public class Translation : ScriptableObject
{
	public static Language language;
	public string[] texts;
}
