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

    Animator animator;
    Vector3 myScale;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isIdleBottom", true);
        myRB = GetComponent<Rigidbody2D>();
        pCondition = GetComponent<PlayerCondition>();
        punchTimer = 0;
        myScale = transform.localScale;
	}

    private void Update()
    {
        if (punchTimer <= 0 && (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1")))
        {
            Punch();
        }

        punchTimer -= Time.deltaTime;

        if(Input.GetAxis("Vertical") != 0) {
            animator.SetBool("Vertical", true);
            if(Input.GetAxis("Vertical") < 0) {
                animator.SetBool("VerticalBottom", true);
            } else {
                animator.SetBool("VerticalBottom", false);
            }
        } else if (Input.GetAxis("Horizontal") != 0) {
            animator.SetBool("VerticalBottom", false);
            animator.SetBool("Vertical", false);
            animator.SetBool("Horizontal", true);
            if (Input.GetAxis("Horizontal") < 0) {                
                transform.localScale = new Vector3(myScale.x * -1, myScale.y, myScale.z);
            } else {
                transform.localScale = myScale;
            }        
        } else {
            animator.SetBool("Vertical", false);
            animator.SetBool("Horizontal", false);
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        Vector2 movement = Vector2.zero;   

        movement.x = Input.GetAxis("Horizontal") * movementSpeed;            

        movement.y = Input.GetAxis("Vertical") * movementSpeed;        

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
