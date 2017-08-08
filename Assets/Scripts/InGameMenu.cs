using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {

	public GameObject pauseMenu;

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (pauseMenu.activeSelf) {
				ResumeGame();
			} else {
				PauseGame();
			}
		}
	}

	public void PauseGame() {
		Time.timeScale = 0;
		Debug.Log("Pause!");

		// Enable pause menu
		pauseMenu.SetActive(true);
	}

	public void ResumeGame() {
		Time.timeScale = 1.0f;

		// Disable pause menu
		pauseMenu.SetActive(false);
	}

	public void QuitToMenu() {
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitGame() {
		Application.Quit();
	}
}
