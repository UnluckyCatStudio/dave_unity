using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores all objects that
/// should persist between
/// scenes.
/// </summary>
public class DontDestroy : MonoBehaviour
{
	public GameObject[] objects;

	public void DestroyAll ()
	{
		for ( int i = 0; i != objects.Length; i++ ) Destroy ( objects[i] );
	} 

	private void Awake ()
	{
		for ( int i = 0; i != objects.Length; i++ ) DontDestroyOnLoad ( objects[i] );
	}
}
