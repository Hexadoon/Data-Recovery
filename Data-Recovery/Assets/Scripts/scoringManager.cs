using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoringManager : MonoBehaviour
{

    public Text Score;
    public Text healthDisplay;
    public Text livesDisplay;
    public float playerHealth = 250;
    float currentHealth;
    public float playerLives = 5;
    public float respawnDelay;
    respawnManager respawnScript;

    
    // Start is called before the first frame update
    void Start()
    {
        healthDisplay.text = "Health: " + playerHealth;
        livesDisplay.text = "Lives: " + playerLives;
        currentHealth = playerHealth;
        respawnScript = gameObject.GetComponent<respawnManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Invoke("respawnPlayer", 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            getHitByenemy();
        }
    }

    void respawnPlayer()
    {
        playerLives--;
        livesDisplay.text = "Lives: " + playerLives;
        currentHealth = playerHealth;
        respawnScript.respawnPlayer();

    }

    void getHitByenemy()
    {
        currentHealth -= 300;
        healthDisplay.text = "Health: " + playerHealth;
    }
}
