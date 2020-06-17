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
    public float playerLives = 6;
    public float respawnDelay;
    public float score = 0;
    respawnManager respawnScript;


    // Start is called before the first frame update
    void Start()
    {

        if (Score == null) {
          Debug.Log("score oogabooga");
          this.enabled = false;
          return;
        }
        Score.text = "Score: " + score;
        healthDisplay.text = "Health: " + playerHealth;
        livesDisplay.text = "Lives: " + playerLives;
        //Lives = GetComponent<Text>();
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
        Debug.Log("playerLives = " + playerLives);

        if (livesDisplay == null) {
          Debug.Log("livesdisplay is junk");
          this.enabled = false;
          return;
        }
        Score.text = "Score: " + score;
        livesDisplay.text = "Lives: " + playerLives;
        //Lives.text = "Lives: " + playerLives;
        currentHealth = playerHealth;
        respawnScript.respawnPlayer();

    }

    void getHitByenemy()
    {
        currentHealth -= 300;
        healthDisplay.text = "Health: " + playerHealth;
    }

}
