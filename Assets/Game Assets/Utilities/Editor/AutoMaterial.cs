using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutoMaterial : EditorWindow
{
	public static AutoMaterial am;
	public static void Init ()
	{
		am = GetWindow<AutoMaterial> ( true, "Auto Material", true );

		if ( Selection.activeGameObject != null )
		{
			am.mat = Selection.activeGameObject.GetComponent<Renderer> ();
			if ( am.mat != null )
			{
				am.prefix = am.mat.sharedMaterials[am.matID].name;
			}
			else am.prefix = "";
		}
		am.path = "";
		am.fullPath = "";
		am.matID = 0;
		am.albedo = true;
		am.metallic = true;
		am.normal = true;
	}

	private Renderer mat;
	private int matID;
	private string prefix;
	private string path;
	private string fullPath;
	private bool albedo;
	private string albedoPath;
	private bool metallic;
	private string metallicPath;
	private bool normal;
	private string normalPath;
	private void OnGUI ()
	{
		EditorGUILayout.LabelField ( "Automatic Material Setup", EditorStyles.boldLabel );

		mat = EditorGUILayout.ObjectField ( "GameObject", mat, typeof ( Renderer ), true ) as Renderer;

		if ( mat != null )
		{
			if ( mat.sharedMaterials.Length > 1 )
			{
				matID = EditorGUILayout.IntSlider ( "Material selector", matID, 0, mat.sharedMaterials.Length-1 );
			}
			prefix = EditorGUILayout.TextField ( "Texture prefix", prefix );
			if ( GUILayout.Button ( "Auto prefix" ) )
			{
				prefix = mat.sharedMaterials[matID].name;
			}

			path = EditorGUILayout.TextField ( "Textures folder", path );
			if ( path != "" && path[path.Length-1] == '/' )
				path = path.Remove ( path.Length-1 );
			fullPath = "Assets/Game Assets/" + path;
			EditorGUILayout.LabelField ( fullPath, EditorStyles.wordWrappedMiniLabel );

			albedoPath      = fullPath + "/" + prefix + "_AlbedoTransparency.png";
			metallicPath    = fullPath + "/" + prefix + "_MetallicSmoothness.png";
			normalPath      = fullPath + "/" + prefix + "_Normal.png";

			EditorGUILayout.LabelField ( "Textures", EditorStyles.boldLabel );

			albedo = EditorGUILayout.ToggleLeft ( "Albedo", albedo );
			if ( albedo )
				EditorGUILayout.LabelField ( prefix + "_AlbedoTransparency.png", EditorStyles.wordWrappedMiniLabel );

			metallic = EditorGUILayout.ToggleLeft ( "Metallic", metallic );
			if ( metallic )
				EditorGUILayout.LabelField ( prefix + "_MetallicSmoothness.png", EditorStyles.wordWrappedMiniLabel );

			normal = EditorGUILayout.ToggleLeft ( "Normal", normal );
			if ( normal )
				EditorGUILayout.LabelField ( prefix + "_Normal.png", EditorStyles.wordWrappedMiniLabel );

			if ( GUILayout.Button ( "Set up!" ) )
			{
				SetUpMaterial ();
			}
		}
	}

	private void SetUpMaterial ()
	{
		var albedoTex = AssetDatabase.LoadAssetAtPath<Texture> ( albedoPath );
		mat.sharedMaterials[matID].SetTexture ( "_MainTex", albedoTex );

		var metallicTex = AssetDatabase.LoadAssetAtPath<Texture> ( metallicPath );
		mat.sharedMaterials[matID].SetTexture ( "_MetallicGlossMap", metallicTex );

		var normalTex = AssetDatabase.LoadAssetAtPath<Texture> ( normalPath );
		mat.sharedMaterials[matID].SetTexture ( "_BumpMap", normalTex );

		MonoBehaviour.print ( albedoPath );
	}
}
