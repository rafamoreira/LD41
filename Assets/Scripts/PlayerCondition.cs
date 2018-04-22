using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCondition : MonoBehaviour {

    bool isInvincible;
    float invincibleTimer;
    Controller pController;
    AudioSource audioPlayer;

    public int currentPlayerPos;
    int health;
    public int initialHealth;
    public List<int> opInContact = new List<int>();
    public AudioClip ouchSound;

    public GameObject directionsBench;

    float replenishTimer = 3;

    public Image healthBar;

    // positions from 0 to 7
	// Use this for initialization
	void Start () {
        directionsBench.SetActive(false);
        healthBar.color = Color.green;        
        health = initialHealth;
        currentPlayerPos = 0;
        isInvincible = false;
        invincibleTimer = 0;
        pController = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        audioPlayer = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        // Set the color of the healthbar.
        float healthPercentage = (1f / initialHealth) * (float)health;
        healthBar.fillAmount = healthPercentage;
        if (healthBar.fillAmount >= 0.5f) {
            healthBar.color = Color.green;
        } else if (healthBar.fillAmount >= .35f) {
            healthBar.color = Color.yellow;
        } else {
            healthBar.color = Color.red;
        }

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

        if(isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            if(invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }

        replenishTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "opponent")
        {
            opInContact.Add(OpponentManager.Instance.opponents.IndexOf(other.gameObject));
        }

        if (other.tag == "Gol")
        {
            GameManager.Instance.GoalScored();
        }

        if(other.tag == "Bench") {
            directionsBench.SetActive(true);
        }

        if(other.tag == "ReplenishLife") {
            ReplenishLife();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "opponent")
        {
            opInContact.Remove(OpponentManager.Instance.opponents.IndexOf(other.gameObject));
        }
        if (other.tag == "Bench") {
            directionsBench.SetActive(false);
        }
    }

    void Dead()
    {   
        StartMatch.Instance.StartCoroutine("PlayerLostAnimation");
        OpponentManager.Instance.StopAll();
        pController.matchRunning = false;
        healthBar.fillAmount = 0;
    }

    public void TakePunch()
    {
        if (!isInvincible)
        {
            audioPlayer.PlayOneShot(ouchSound);
            health -= 1;


            if (health <= 0)
            {

                Dead();
            }
            else
            {
                isInvincible = true;
                invincibleTimer = 1f;
            }
        }
    }

    void ReplenishLife() {
        print("enter replenish");
        if(health >= initialHealth) {
            return;
        }
        if (replenishTimer <= 0) {
            replenishTimer = 3;
            print("Add a health");
            health++;
        }
    }
}
