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

	#region Graphics

	// Resolution
	public InputField height;
	public InputField width;

	public Toggle vsync;
	public Slider textures;
	public Slider shadows;
	public Toggle antialiasing;
	public Slider postFX;
	public Slider FOV;

	#endregion

	public void Resume ()
	{
		Game.mPause.SwitchPause ();
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

	#region Main Menu

	public void Play ()
	{
		SceneManager.LoadScene ( "dave_tests" );
		pauseMenu.SetActive ( false );
		mainMenu.SetActive ( false );
	}

	public void Exit ()
	{
		Application.Quit ();
	}

	#endregion
}
