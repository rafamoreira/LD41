using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuCanvas : MonoBehaviour {
    public void Quit() {
        Application.Quit();
    }

    public void StartGame() {
        SceneManager.LoadScene("Level1");

    }    
}
