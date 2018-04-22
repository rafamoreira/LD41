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
        StartMatch.Instance.StartCoroutine("GoalAnimation");
        OpponentManager.Instance.StopAll();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().matchRunning = false;
        Time.timeScale = 0;
        goals += 1;
        SceneManager.LoadScene("2");
    }


    public void OpponentDown()
    {
        opponentsDown += 1;
    }

}
