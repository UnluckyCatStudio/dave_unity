using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// FX and UI controls
/// </summary>
public class UIManager : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject pauseMenu;

	#region Pause Menu

	#region Controls
	[Header("Controls")]
	public Text[] hotkeys;
	#endregion

	#region Graphics
	[Header("Graphics")]
	public Dropdown		resolutions;
	public Toggle		vsync;
	public Slider		textures;
	public Slider		postFX;
	public Slider		shadows;
	public Slider		FOV;
	public Toggle		antialiasing;
	#endregion

	#region FX
	public void Resume ()
	{
		Game.pause.SwitchPause ();
	}
	
	public void QuitToMainMenu ()
	{
#if DEMO_V_0_1
		Time.timeScale = 1;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		SceneManager.LoadScene ( "MainMenuBG_DEMO" );
		GetComponent<DontDestroy> ().DestroyAll ();
#else
		SceneManager.LoadScene ( "EmptyUIScene" );
		Time.timeScale = 1;
#endif
	}
	#endregion

	#endregion

	#region Main Menu

	#region FX
	public void Play ()
	{
#if DEMO_V_0_1
		SceneManager.LoadScene ( "dave_tests" );
#else

#endif
		pauseMenu.SetActive ( false );
		mainMenu.SetActive ( false );
	}

	public void Exit ()
	{
		Application.Quit ();
	}
	#endregion

	#endregion
}
