using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlacetaParams : MonoBehaviour
{
	public MeshRenderer fog;
	private ProceduralMaterial mat;

	[Header ( "Params" )]
	[Range ( 0, 100 )] public float disorder;
	[Range ( 0, 100 )] public float flow;
	[Range ( 0, 1 )] public float width;
	[Range ( 0, 1 )] public float lenght;

	private void Update ()
	{
		mat.SetProceduralFloat ( "Disorder", disorder );
		mat.SetProceduralFloat ( "Flow", flow );
		mat.SetProceduralFloat ( "Pattern_Lenght", lenght );
		mat.SetProceduralFloat ( "Pattern_Width", width );
		mat.RebuildTextures ();
	}

	private void OnEnable ()
	{
		mat = fog.sharedMaterial as ProceduralMaterial;
		mat.cacheSize = ProceduralCacheSize.Medium;
	}
}
