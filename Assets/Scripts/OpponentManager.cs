using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentManager : MonoBehaviour {

    private static OpponentManager _instance;

    public static OpponentManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<OpponentManager>();
            }

            return _instance;
        }
    }

    public List<GameObject> opponents = new List<GameObject>();

    public void GivePunch(int position)
    {
        opponents[position].gameObject.GetComponent<OpponentBehavior>().TakePunch();
    }
}
