using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    
    Rigidbody2D myRB;
    PlayerCondition pCondition;
    float punchTimer;

    public float movementSpeed;
    public float punchDelay;


    // Use this for initialization
    void Start ()
    {
        myRB = GetComponent<Rigidbody2D>();
        pCondition = GetComponent<PlayerCondition>();
        punchTimer = 0;
	}

    private void Update()
    {
        if (punchTimer <= 0 && (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1")))
        {
            Punch();
        }

        punchTimer -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        Vector2 movement = Vector2.zero;
        if (Input.GetAxis("Horizontal") != 0)
        {
            movement.x = Input.GetAxis("Horizontal") * movementSpeed;
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            movement.y = Input.GetAxis("Vertical") * movementSpeed;
        }

        myRB.MovePosition(myRB.position + movement * Time.fixedDeltaTime);
    }

    void Punch()
    {
        if (pCondition.opInContact.Count > 0)
        {
            // get the index of opponentsInContact array
            int randomPunch = Random.Range(0, pCondition.opInContact.Count);
            // send now the index of players on the opponents manager
            OpponentManager.Instance.GivePunch(pCondition.opInContact[randomPunch]);
            // remove the opponent from the opponentsInContact array on player condition
            pCondition.OpponentPunched(randomPunch);
        }
        punchTimer = punchDelay;
    }
}
