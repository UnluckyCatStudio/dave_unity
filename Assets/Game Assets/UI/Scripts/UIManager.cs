using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

/// <summary>
/// FX and UI controls
/// </summary>
public class UIManager : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject pauseMenu;

	#region Pause Menu

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

	#region AUDIO
	[Header ("Audio")]
	public AudioMixer audioMaster;
	public Slider master;
	public Slider music;
	public Slider sfx;
	public Slider ambient;
	public Slider voices;
	#endregion

	#region Controls
	[Header("Controls")]
	public Text[] hotkeys;
	#endregion

	#region FX
	// IDs of quality info texts
	// for translation.
	string[] infoRef =
	{
		"pause_menu:low",
		"pause_menu:medium",
		"pause_menu:high",
		"pause_menu:veryhigh"
	};
	public void UpdateInfoText ( Text info )
	{
		var value = ( int ) info.transform.parent.GetComponent<Slider> ().value;
		info.text = Localizator.GetText ( infoRef[value] );
	}

	public void UpdateInfoInt ( Text info )
	{
		//if ( !info.gameObject.activeInHierarchy ) return;

		var value = ( int ) info.transform.parent.GetComponent<Slider> ().value;
		info.text = value.ToString ();
	}

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
		throw new System.NotImplementedException ( "FALTA MAIN MENU LOL" );
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
