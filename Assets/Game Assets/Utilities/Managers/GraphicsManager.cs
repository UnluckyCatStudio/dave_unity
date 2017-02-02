using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GraphicsSettings
{
	public int  resolution;
	public bool fullscreen;
	public bool vsync;
	public int  textures;
	public int  postFX;
	public int  shadows;
	public int  fov;
	public bool antialising;
}

public class GraphicsManager : MonoBehaviour
{
	// Default values
	public int	resolution		= 0;
	public bool fullscreen      = true;
	public bool vsync			= true;
	public int	textures		= 3;
	public int	postFX			= 3;
	public int	shadows			= 3;
	public int	fov				= 60;
	public bool antialising		= true;

	public void LoadValues ()
	{
		Game.ui.resolutions.value		= resolution;
		Game.ui.fullscreen.isOn			= fullscreen;
		Game.ui.vsync.isOn				= vsync;
		Game.ui.textures.value			= textures;
		Game.ui.shadows.value			= shadows;
		Game.ui.antialiasing.isOn		= antialising;
		Game.ui.postFX.value			= postFX;
		Game.ui.FOV.value				= fov;
	}

	public void ApplySave ( bool justApply = false )
	{
		// Apply
		resolution	= Game.ui.resolutions.value;
		fullscreen	= Game.ui.fullscreen.isOn;
		vsync		= Game.ui.vsync.isOn;
		textures	= ( int ) Game.ui.textures.value;
		postFX		= ( int ) Game.ui.postFX.value;
		shadows		= ( int ) Game.ui.shadows.value;
		fov			= ( int ) Game.ui.FOV.value;
		antialising = Game.ui.antialiasing.isOn;

		Apply ();
		if ( justApply ) return;

		// Save
        PlayerPrefs.SetString ( "Graphics", JsonUtility.ToJson ( this ) );
        PlayerPrefs.Save ();
	}

	private void Apply ()
	{
		// Apply changes to engine
		Screen.SetResolution ( Screen.resolutions[resolution].width, Screen.resolutions[resolution].height, fullscreen );
		QualitySettings.vSyncCount = vsync ? 1 : 0;
		QualitySettings.masterTextureLimit = Math.Abs ( textures - 3 );		// Correct slider value
		QualitySettings.shadowResolution = ( ShadowResolution ) shadows;
		// TODO:
		// AA, PostFX & FoV
		// is controlled per camera
	}

	public void LoadResolutions ()
	{
		for ( int i = 0; i != Screen.resolutions.Length; i++ )
		{
			var txt = Screen.resolutions[i].width + " x " + Screen.resolutions[i].height;
			var res = new UnityEngine.UI.Dropdown.OptionData ( txt );
			Game.ui.resolutions.options.Add ( res );
		}
	}
}
