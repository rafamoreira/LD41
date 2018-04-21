using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public float movementSpeed;
    Rigidbody2D myRB;


	// Use this for initialization
	void Start ()
    {
        myRB = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
        {
            Debug.Log("Punch !!!");
        }
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
}
