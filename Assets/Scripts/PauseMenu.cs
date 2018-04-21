using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public bool gamePaused;
    GameObject pauseMenu;

    

    void Start() {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseCanvas");
        pauseMenu.SetActive(false);
        gamePaused = false;
        
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKey(KeyCode.P)) {
            if (gamePaused) {
                Resume();
            } else {
                Pause();
            }
        }
	}

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused  = false;
    }

    void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void MainMenu() {
        // Got to main menu
        SceneManager.LoadScene("NameOfTheScene");
    }
    public void Exit() {
        Application.Quit();
    }


}
