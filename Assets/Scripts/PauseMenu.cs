using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public bool gamePaused;
    GameObject pauseMenu, restartMenu;    

    void Start() {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseCanvas");
        restartMenu = GameObject.FindGameObjectWithTag("RestartCanvas");
        pauseMenu.SetActive(false);
        gamePaused = false;
        restartMenu.SetActive(false);        
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
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

    public void RestartPanel() {
        restartMenu.SetActive(true);
        Time.timeScale = 0f;        
    }

    public void MainMenu() {
        // Got to main menu
        SceneManager.LoadScene("MainMenu");
    }
    public void Exit() {
        Application.Quit();
    }

    public void Restart() {
        //Restart the game
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


}
