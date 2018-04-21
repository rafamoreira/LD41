using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour {

    public int currentPlayerPos;
    // positions from 0 to 7
	// Use this for initialization
	void Start () {
        currentPlayerPos = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > -4.2 && currentPlayerPos < 1)
        {
            currentPlayerPos = 1;
        }
        else if (transform.position.y > -2.52 && currentPlayerPos < 2)
        {
            currentPlayerPos = 2;
        }
        else if (transform.position.y > -0.84 && currentPlayerPos < 3)
        {
            currentPlayerPos = 3;
        }
        else if (transform.position.y > 0.84 && currentPlayerPos < 4)
        {
            currentPlayerPos = 4;
        }
        else if (transform.position.y > 2.52 && currentPlayerPos < 5)
        {
            currentPlayerPos = 5;
        }
        else if (transform.position.y > 4.2 && currentPlayerPos < 6)
        {
            currentPlayerPos = 6;
        }
	}
}
