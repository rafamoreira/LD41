﻿using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class StartMatch : MonoBehaviour {

    float matchTime;
    bool matchStarted;
    [Header("Scoreboard")]
    public GameObject scoreBoard;
    [Header("MathStart Animation")]
    public GameObject panelForTexts;
    public Text matchTimeText, textMatchStart;
    [Header("Sounds")]
    public AudioSource ambientSound;
    
    [Header("Debugging")]
    public bool goalScoredTest;

	// Use this for initialization
	void Start () {
        StartCoroutine(StartMatchAnimation());
        
	}
	
	// Update is called once per frame
	void Update () {
        if (matchStarted) {
            StartMatchTime();
        }
        if (goalScoredTest) {
            StartCoroutine(GoalAnimation());
        }
	}

    //Call this on the start of each match
    IEnumerator StartMatchAnimation() {
        scoreBoard.SetActive(false);
        panelForTexts.SetActive(true);
        textMatchStart.text = "3";
        yield return new WaitForSeconds(1f);
        textMatchStart.text = "2";
        yield return new WaitForSeconds(1f);
        textMatchStart.text = "1";
        yield return new WaitForSeconds(1f);
        textMatchStart.gameObject.transform.localScale = new Vector3(0.07f, 0.5f, 0.7f);
        textMatchStart.text = "Match started";
        yield return new WaitForSeconds(1f);
        panelForTexts.SetActive(false);
        matchStarted = true;
    }

    //Call this coroutine when a goal is scored
    public IEnumerator GoalAnimation() {
        scoreBoard.SetActive(false);
        panelForTexts.SetActive(true);
        textMatchStart.text = "Goal";
        yield return new WaitForSeconds(3f);
        panelForTexts.SetActive(false);        
        scoreBoard.SetActive(true);
        goalScoredTest = false;
    }

    void StartMatchTime() {
        scoreBoard.SetActive(true);
        matchTime += Time.deltaTime;
        matchTimeText.text = Mathf.RoundToInt(matchTime).ToString();
    }
}