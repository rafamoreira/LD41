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
    bool isChasing;

    float healthPercentage;

    // Use this for initialization
    void Start ()
    {
        //Set color of healthbar to green
        healthIndicator.color = Color.green;

        pCondition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();

        speed = Random.Range(0.1f, 1.5f);
        isActive = false;
        animator = GetComponent<Animator>();

        myScale = transform.localScale;
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
        transform.position = Vector2.MoveTowards(transform.position, pCondition.transform.position, speed * Time.deltaTime);
        ChaseAnim();
        isChasing = true;
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
            healthPercentage = (1f / initialHealth) * (float)health;
            healthIndicator.fillAmount = healthPercentage;
            if (healthIndicator.fillAmount >= 0.5f) {
                healthIndicator.color = Color.green;
            } else if (healthIndicator.fillAmount >= .35f) {
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
        StopCoroutine("CheckPunchPlayer");
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
                // Gambiara para manter o canvas sem inverter o tamanho
                Transform child = GetComponentInChildren<Transform>();
                Vector3 childScale = child.transform.localScale;
                // Flipa o personagem
                transform.localScale = new Vector3(myScale.x * -1, myScale.y, myScale.z);
                // Desflipa o canvas
                child.localScale = childScale;
            } else {
                transform.localScale = myScale;
            }
        }
    }

    void PunchPlayer()
    {
        pCondition.TakePunch();
    }

    IEnumerator CheckPunchPlayer()
    {
        while(true)
        {
            if(isChasing)
            {
                float distance = Vector3.Distance(transform.position, pCondition.transform.position);
                Debug.Log(distance);
                if (distance <= 0.25)
                {
                    PunchPlayer();
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
