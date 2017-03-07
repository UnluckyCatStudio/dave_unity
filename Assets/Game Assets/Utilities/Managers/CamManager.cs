using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CinematicEffects;

public class CamManager : MonoBehaviour
{
	/// <summary>
	/// List of all 4 cameras.
	/// - Low
	/// - Medium
	/// - High
	/// - Really high
	/// </summary>
	public  GameObject[] cameras;

	private GameObject   active;
	private int          activeID=3;

	/// <summary>
	/// Happens when graphic settings change so the
	/// camera rig is adjusted to them.
	/// </summary>
	public void UpdateRig () 
	{
		// Disable currently active camera
		cameras[activeID].SetActive ( false );

		// Enable correct camera and save it
		activeID = Game.graphics.postFX;
		active = cameras[activeID];
		active.SetActive ( true );

		// AA and FoV
		active.GetComponent<Camera> ().fieldOfView   = Game.graphics.fov;
		active.GetComponent<AntiAliasing> ().enabled = Game.graphics.antialising;
	}

	// Setup game reference
	// First update!
	private void Awake () 
	{
		Game.cam = this;
		UpdateRig ();
	}
}
