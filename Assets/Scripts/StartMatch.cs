﻿using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMatch : MonoBehaviour {

    private static StartMatch _instance;

    public static StartMatch Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<StartMatch>();
            }

            return _instance;
        }
    }

    float matchTime;
    bool matchStarted;
    [Header("Scoreboard")]
    public GameObject scoreBoard;
    [Header("MathStart Animation")]
    public GameObject panelForTexts;
    public Text matchTimeText, textMatchStart;

    [Header("Sounds")]
    public AudioClip whisteSound;
    AudioSource ambientSound;

    [Header("Pause Menu")]
    PauseMenu pauseMenu;

    Controller pController;

	// Use this for initialization
	void Start () {
       if(SceneManager.GetActiveScene().buildIndex <= 6) {            
            StartCoroutine(StartMatchAnimation());
            pController = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        }
        pauseMenu = GetComponent<PauseMenu>();        
        ambientSound = GetComponent<AudioSource>();
        ambientSound.Play();
        
        Time.timeScale = 1;

    }
	
	// Update is called once per frame
	void Update () {
        if (matchStarted) {
            StartMatchTime();
        }
	}

    //Call this on the start of each match
    IEnumerator StartMatchAnimation() {
        scoreBoard.SetActive(false);
        panelForTexts.SetActive(true);
        textMatchStart.text = "3";
        yield return new WaitForSeconds(1f);
        textMatchStart.text = "2";
        ambientSound.Play();
        yield return new WaitForSeconds(1f);
        textMatchStart.text = "1";
        yield return new WaitForSeconds(1f);
        textMatchStart.gameObject.transform.localScale = new Vector3(0.07f, 0.5f, 0.7f);
        ambientSound.PlayOneShot(whisteSound, 1f);
        textMatchStart.text = "Match started";
        yield return new WaitForSeconds(1f);
        panelForTexts.SetActive(false);
        matchStarted = true;
        pController.matchRunning = true;
    }

    //Call this coroutine when a goal is scored
    public IEnumerator GoalAnimation() {
        scoreBoard.SetActive(false);
        panelForTexts.SetActive(true);
        ambientSound.PlayOneShot(whisteSound, 1f);
        textMatchStart.text = "Goal";
        yield return new WaitForSeconds(3f);
        panelForTexts.SetActive(false);        
        scoreBoard.SetActive(true);
        GameManager.Instance.NextScene();
    }

    //Call this coroutine when a goal is scored
    public IEnumerator PlayerLostAnimation() {
        ambientSound.PlayOneShot(whisteSound, 1f);
        scoreBoard.SetActive(false);
        panelForTexts.SetActive(true);
        textMatchStart.text = "You Lost";
        yield return new WaitForSeconds(3f);
        panelForTexts.SetActive(false);        
        pauseMenu.RestartPanel();
    }

    public IEnumerator ShowScore() {
        ambientSound.PlayOneShot(whisteSound, 1f);
        scoreBoard.SetActive(false);
        panelForTexts.SetActive(true);

        yield return new WaitForSeconds(3f);
        panelForTexts.SetActive(false);        
        pauseMenu.RestartPanel();
    }


    void StartMatchTime() {
        scoreBoard.SetActive(true);
        matchTime += Time.deltaTime;
        matchTimeText.text = Mathf.RoundToInt(matchTime).ToString();
    }
}
