using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class activarmenu : MonoBehaviour {

	public GameObject pausePanel;
	public bool isPaused;
	public GameObject pausemenu;

	// Use this for initialization
	void Start () {
			
		isPaused = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused) {
			PauseGame (true);
		} else {
			PauseGame (false);
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			pausePanel.SetActive (true);
			pausemenu.SetActive (true);
			var blur = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Blur> ();
			blur.enabled = !blur.enabled;
			SwitchPause ();
		}

	}

	public void CargarNivel (string pMenu){
		SceneManager.LoadScene (pMenu);
	
	}

	void PauseGame (bool state){
		if (state) {
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}
		pausePanel.SetActive (state);
	}
		public void SwitchPause(){
		if (isPaused) {
			isPaused = false;
		}else{
			isPaused = true;
	}
}
}