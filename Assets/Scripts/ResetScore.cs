using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScore : MonoBehaviour {

    void Start() {
        GameManager.Instance.ResetScore();

    }
}
