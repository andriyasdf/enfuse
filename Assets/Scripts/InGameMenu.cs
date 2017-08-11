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
		pauseMenu.SetActive(true);
	}

	public void ResumeGame() {
		pauseMenu.SetActive(false);
	}

	public void QuitToMenu() {
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitGame() {
		Application.Quit();
	}
}
