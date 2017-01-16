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
	public InputField forward;
	public InputField backwards;
	public InputField jump;
	public InputField backwards_jump;

	// Converts Controls input to Upper
	// Also saves all the keys
	public void ToUpper ( InputField control )
	{
		if ( control.text != "" && control.text != " " )
		{
			control.text = control.text.ToUpper ();
			Game.input.ApplySave (); 
		}
	}

	#endregion

	#region Graphics
	[Header("Graphics")]
	public Dropdown resolutions;
	public Toggle vsync;
	public Slider textures;
	public Slider shadows;
	public Toggle antialiasing;
	public Slider postFX;
	public Slider FOV;

	#endregion

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
