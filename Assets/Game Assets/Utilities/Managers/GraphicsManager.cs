using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
	// Default values
	public int	resolution		= 0;
	public bool vsync			= true;
	public int	textures		= 3;
	public int	postFX			= 3;
	public int	shadows			= 0;
	public int	fov				= 60;
	public bool antialising		= true;

	public void LoadValues ()
	{
		Game.ui.resolutions.value		= resolution;
		Game.ui.vsync.isOn				= vsync;
		Game.ui.textures.value			= textures;
		Game.ui.shadows.value			= shadows;
		Game.ui.antialiasing.isOn		= antialising;
		Game.ui.postFX.value			= postFX;
		Game.ui.FOV.value				= fov;
	}

	public void Apply ()
	{
		// Apply changes to engine
		Screen.SetResolution ( Screen.resolutions[resolution].width, Screen.resolutions[resolution].height, true );
		QualitySettings.vSyncCount = 1;
		QualitySettings.masterTextureLimit = Math.Abs ( textures );
		QualitySettings.shadowResolution = ( ShadowResolution ) shadows;
		// TODO:
		// AA, PostFX & FoV
		// is controlled per camera

		SaveValues ();
	}

	private void SaveValues ()
	{
		resolution	= Game.ui.resolutions.value;
		vsync		= Game.ui.vsync.isOn;
		textures	= ( int ) Game.ui.textures.value;
		postFX		= ( int ) Game.ui.postFX.value;
		shadows		= ( int ) Game.ui.shadows.value;
		fov			= ( int ) Game.ui.FOV.value;
		antialising = Game.ui.antialiasing.isOn;

        PlayerPrefs.SetString ( "Graphics", JsonUtility.ToJson ( this ) );
        PlayerPrefs.Save ();
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
