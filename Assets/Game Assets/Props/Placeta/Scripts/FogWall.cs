using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FogWall : MonoBehaviour
{
	public Color color;
	private new MeshRenderer renderer;

	private void Update () 
	{
		if (!renderer) Awake ();

		renderer.sharedMaterial.SetColor ( "_Main", color );
	}

	private void Awake () 
	{
		renderer = GetComponent<MeshRenderer> ();
	}
}
