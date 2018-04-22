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
    bool isActive;
    float stunClock;
    bool isStunned;
    int health;
    bool isChasing;

    float healthPercentage;



    // Use this for initialization
    void Start ()
    {
        //Set color of healthbar to green
        healthIndicator.color = Color.green;

        pCondition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();

        if (opponentType == OpponentType.GK)
            speed = 2;
        else
            speed = Random.Range(0.1f, 1.5f);

        isActive = false;
        animator = GetComponent<Animator>();
        health = initialHealth;
        stunClock = 0.5f;
        isStunned = true;
        healthIndicator.fillAmount = 1;
        StartCoroutine("CheckPunchPlayer");
        isChasing = false;
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
        if(opponentType == OpponentType.GK)
        {
            float pDistance = Vector3.Distance(transform.position, pCondition.transform.position);

            if (pDistance < 1.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, pCondition.transform.position, speed * Time.deltaTime);
                isChasing = true;
                ChaseAnim();
            }   
            else
            {
                Vector2 moveDir = Vector2.MoveTowards(transform.position, new Vector3(0, 5.6f, 0), speed * Time.deltaTime);
                transform.position = moveDir;
                if (moveDir == new Vector2(0, 5.6f))
                    IdleAnim();
                else
                    ChaseAnim();
                

                isChasing = false;
            }
                

            Vector3 targetPos = transform.position;

            if (transform.position.y < 4.5f)
                targetPos.y = 4.5f;
            if (transform.position.x > 2)
                targetPos.x = 2;
            if (transform.position.x < -2)
                targetPos.x = -2;

            transform.position = targetPos;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pCondition.transform.position, speed * Time.deltaTime);
            ChaseAnim();
            isChasing = true;
        }
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
            if (opponentType != OpponentType.GK)
                Stun();

            healthPercentage = (1f / initialHealth) * (float)health;
            healthIndicator.fillAmount = healthPercentage;
            if (healthIndicator.fillAmount >= 0.8f) {
                healthIndicator.color = Color.green;
            } else if (healthIndicator.fillAmount >= .5f) {
                healthIndicator.color = Color.yellow;
            } else {
                healthIndicator.color = Color.red;
            }
        }


    }

    void Stun()
    {
        stunClock = 0.5f;
        isActive = false;
        isStunned = true;
        isChasing = false;
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
        isChasing = false;
        healthIndicator.fillAmount = 0;
        GameManager.Instance.OpponentDown();
        StopCoroutine("CheckPunchPlayer");
    }

    void ChaseAnim()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
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
                
                sprite.flipX = true;

            } else {
                sprite.flipX = false;
            }
        }
    }

    void PunchPlayer()
    {
        pCondition.TakePunch();
    }

    public void EndGame()
    {
        isActive = false;
        isChasing = false;
        isStunned = false;
        StopAllCoroutines();
        IdleAnim();
    }

    IEnumerator CheckPunchPlayer()
    {
        while(true)
        {
            if(isChasing)
            {
                float distance = Vector3.Distance(transform.position, pCondition.transform.position);
                if (distance <= 0.25)
                {
                    PunchPlayer();
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
