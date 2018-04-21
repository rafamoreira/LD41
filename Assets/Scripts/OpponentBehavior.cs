using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentBehavior : MonoBehaviour {

    public enum OpponentType { GK, DC, MDC, MC, MAC, FW }

    public OpponentType opponentType;
    public float speed;

    PlayerPosition pPosition;
    

	// Use this for initialization
	void Start () {
        pPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPosition>();

    
        speed = Random.Range(0.1f, 1.5f);
    
	}
	
	// Update is called once per frame
	void Update () {
		if (opponentType == OpponentType.FW && pPosition.currentPlayerPos > 0)
        {
            Chase();
        }
        else if (opponentType == OpponentType.MAC && pPosition.currentPlayerPos > 1)
        {
            Chase();
        }
        else if (opponentType == OpponentType.MC && pPosition.currentPlayerPos > 2)
        {
            Chase();
        }
        else if (opponentType == OpponentType.MDC && pPosition.currentPlayerPos > 3)
        {
            Chase();
        }
        else if (opponentType == OpponentType.DC && pPosition.currentPlayerPos > 4)
        {
            Chase();
        }
        else if (opponentType == OpponentType.GK && pPosition.currentPlayerPos > 5)
        {
            Chase();
        }
	}

    void Chase()
    {
        //transform.LookAt(pPosition.transform);
        transform.position = Vector2.MoveTowards(transform.position, pPosition.transform.position, speed * Time.deltaTime);
        Debug.Log("Chase " + opponentType.ToString() + " " + gameObject.name);
    }
}
