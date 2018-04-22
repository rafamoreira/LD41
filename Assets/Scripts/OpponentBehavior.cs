using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentBehavior : MonoBehaviour {

    public enum OpponentType { GK, DC, MDC, MC, MAC, FW }

    public OpponentType opponentType;
    public float speed;
    public int initialHealth;
    public Image healthIndicator;

    PlayerCondition pCondition;
    Animator animator;
    Vector3 myScale;
    bool isActive;
    float stunClock;
    bool isStunned;
    int health;

    // Use this for initialization
    void Start ()
    {
        pCondition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();

        speed = Random.Range(0.1f, 1.5f);
        isActive = true;
        animator = GetComponent<Animator>();

        myScale = transform.localScale;
        health = initialHealth;
        stunClock = 0.5f;
        isStunned = false;
        healthIndicator.fillAmount = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if(isActive)
        {
            if (opponentType == OpponentType.FW && pCondition.currentPlayerPos > 0)
            {
                Chase();
            }
            else if (opponentType == OpponentType.MAC && pCondition.currentPlayerPos > 1)
            {
                Chase();
            }
            else if (opponentType == OpponentType.MC && pCondition.currentPlayerPos > 2)
            {
                Chase();
            }
            else if (opponentType == OpponentType.MDC && pCondition.currentPlayerPos > 3)
            {
                Chase();
            }
            else if (opponentType == OpponentType.DC && pCondition.currentPlayerPos > 4)
            {
                Chase();
            }
            else if (opponentType == OpponentType.GK && pCondition.currentPlayerPos > 5)
            {
                Chase();
            }
        }

        if (isStunned)
        {
            stunClock -= Time.deltaTime;

            if (stunClock <= 0)
            {
                isActive = true;
                isStunned = false;
            }
        }
	}

    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, pCondition.transform.position, speed * Time.deltaTime);
        ChaseAnim();
    }

    public void TakePunch()
    {
        health -= 1;

        if (health <= 0)
        {
            Dead();
        }
        else
        {
            Stun();
            float healthPercentage = (1f / initialHealth) * (float)health;
            healthIndicator.fillAmount = healthPercentage;
        }


    }

    void Stun()
    {
        stunClock = 0.5f;
        isActive = false;
        isStunned = true;
        IdleAnim();
    }

    void IdleAnim()
    {
        animator.SetBool("Vertical", false);
        animator.SetBool("VerticalBottom", false);
        animator.SetBool("Horizontal", false);
    }

    void Dead()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        IdleAnim();
        isActive = false;
        healthIndicator.fillAmount = 0;
    }

    void ChaseAnim()
    {

        if (pCondition.transform.position.y > transform.position.y + 0.2f)
        {
            // running up animation
            animator.SetBool("Vertical", true);
            animator.SetBool("VerticalBottom", false);
            animator.SetBool("Horizontal", false);
        }
        else if (pCondition.transform.position.y < transform.position.y - 0.2f)
        {
            // running down animation
            animator.SetBool("Vertical", true);
            animator.SetBool("VerticalBottom", true);
            animator.SetBool("Horizontal", false);
        }
        else 
        {
            animator.SetBool("Vertical", false);
            animator.SetBool("VerticalBottom", false);
            animator.SetBool("Horizontal", true);
            if (pCondition.transform.position.x < transform.position.x) {
                transform.localScale = new Vector3(myScale.x * -1, myScale.y, myScale.z);
            } else {
                transform.localScale = myScale;
            }
        }
    }
}
