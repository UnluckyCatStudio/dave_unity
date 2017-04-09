using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu ("Image Effects/Posterize")]
public class Posterize : ImageEffectBase
{
	[Range (1f, 64f)]	public float amountOfColors;
	[Range (0.01f, 2f)] public float gammaValue;

	void OnRenderImage ( RenderTexture source, RenderTexture destination )
	{
		material.SetFloat ( "_Colors", amountOfColors );
		material.SetFloat ( "_Gamma", gammaValue );
		Graphics.Blit ( source, destination, material );
	}
}
