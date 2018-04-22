using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallEndGame : MonoBehaviour {

    public Text textMatchStart;

	// Use this for initialization
	void Start () {
        textMatchStart.text = "Your Scored " + GameManager.Instance.goals + " goals" + "\n" +  "and punched " +
                GameManager.Instance.opponentsDown + " players";
    }

}
