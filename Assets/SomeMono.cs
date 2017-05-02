using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeMono : MonoBehaviour
{
	public enum Hey
	{
		Hey,
		Im,
		an,
		Enum
	}

	public static bool myBool;
	public static Color myColor;
	public static string myString;
	public static float myFloat;
	public static Hey myEnum;

	public bool nonStaticBool;
	public Color anotherColor;
	public Object whatever;
}
