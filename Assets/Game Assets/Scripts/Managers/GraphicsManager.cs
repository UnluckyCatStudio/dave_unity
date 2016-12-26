using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
	// Default values
	public int height=1920, width=1080;

	public bool vsync = true;
	public int textures = 3;
	public int shadows = 3;
	public bool antialising = true;
	public int postFX = 3;
	public int FOV = 60;

	public void LoadValues()
	{
		Game.mUI.height.text = height.ToString (); Game.mUI.width.text = width.ToString ();

		Game.mUI.vsync.isOn = vsync;
		Game.mUI.textures.value = textures;
		Game.mUI.shadows.value = shadows;
		Game.mUI.antialiasing.isOn = antialising;
		Game.mUI.postFX.value = postFX;
		Game.mUI.FOV.value = FOV;
	}

	public void Apply()
	{
		
	}

	public void SaveValues()
	{
		
	}
}
