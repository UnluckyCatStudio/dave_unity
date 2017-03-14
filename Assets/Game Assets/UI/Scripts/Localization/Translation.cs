﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language 
{
	EN,
	ES,
	CAT
}

// SHOULD ONLY HAVE ONE FOR
// EACH LANGUAGE

[CreateAssetMenu]
public class Translation : ScriptableObject
{
	public Language language;
	public string[] texts;
}
