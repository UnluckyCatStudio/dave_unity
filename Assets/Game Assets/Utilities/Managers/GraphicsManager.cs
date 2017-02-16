using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kyru.UI;

[Serializable]
public struct GraphicSettings 
{
	public int  resolution;
	public bool fullscreen;
	public bool vsync;
	public int  textures;
	public int  postFX;
	public int  shadows;
	public int  fov;
	public bool antialising;

	/// <summary>
	/// Sets this struct to default
	/// graphics settings.
	/// </summary>
	public void SetDefaults () 
	{
		resolution	= 0;
		fullscreen	= true;
		vsync		= true;
		textures	= 3;
		postFX      = 3;
		shadows     = 3;
		fov         = 60;
		antialising = true;
	}
}

public class GraphicsManager : MonoBehaviour
{
	#region UI
	public Dropdown resolution;
	public Toggle	fullscreen;
	public Toggle	vsync;
	public Slider	textures;
	public Slider	postFX;
	public Slider	shadows;
	public Slider	fov;
	public Toggle	antialiasing;
	#endregion

	public void LoadValues () 
	{
		resolution.value	= Game.graphics.resolution;
		fullscreen.isOn		= Game.graphics.fullscreen;
		vsync.isOn			= Game.graphics.vsync;
		textures.value		= Game.graphics.textures;
		shadows.value		= Game.graphics.shadows;
		antialiasing.isOn	= Game.graphics.antialising;
		postFX.value		= Game.graphics.postFX;
		fov.value			= Game.graphics.fov;
	}

	public void ApplySave ( bool justApply = false ) 
	{
		// Apply
		Game.graphics.resolution	= resolution.value;
		Game.graphics.fullscreen	= fullscreen.isOn;
		Game.graphics.vsync			= vsync.isOn;
		Game.graphics.textures		= ( int ) textures.value;
		Game.graphics.postFX		= ( int ) postFX.value;
		Game.graphics.shadows		= ( int ) shadows.value;
		Game.graphics.fov			= ( int ) fov.value;
		Game.graphics.antialising	= antialiasing.isOn;

		Apply ();
		if ( justApply ) return;

		// Save
        PlayerPrefs.SetString ( "Graphics", JsonUtility.ToJson ( Game.graphics ) );
        PlayerPrefs.Save ();
	}

	private void Apply () 
	{
		// Apply changes to engine
		Screen.SetResolution ( Screen.resolutions[resolution.value].width, Screen.resolutions[resolution.value].height, fullscreen.isOn );
		QualitySettings.vSyncCount = vsync.isOn ? 1 : 0;
		QualitySettings.masterTextureLimit = ( int ) Math.Abs ( textures.value - 3 );		// Correct slider value
		QualitySettings.shadowResolution = ( ShadowResolution ) shadows.value;
		// TODO:
		// AA, PostFX & FoV
		// is controlled per camera
	}

	public void LoadResolutions () 
	{
		// Clear first Editor options
		resolution.options.Clear ();

		for ( int i = 0; i != Screen.resolutions.Length; i++ )
		{
			var txt = Screen.resolutions[i].width + " x " + Screen.resolutions[i].height;
			var res = new Dropdown.OptionData ( txt );
			resolution.options.Add ( res );
		}
	}
}
