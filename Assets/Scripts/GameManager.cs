using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    public int opponentCurId;
    public int opponentsDown;
    public int goals;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);        
    }

    public void GoalScored()
    {
        OpponentManager.Instance.StopAll();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().matchRunning = false;
        goals += 1;
        StartMatch.Instance.StartCoroutine("GoalAnimation");        
        //Time.timeScale = 0;
    }

    public void NextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                
        SceneManager.LoadScene("Level" + currentSceneIndex);
    }

    public void OpponentDown()
    {
        opponentsDown += 1;
    }
    
    public void ResetScore() {
        goals = 0;
        opponentsDown = 0;
    }
}
