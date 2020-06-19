using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreboard : MonoBehaviour
{

    public Text Score;
    public Text healthDisplay;
    public Text livesDisplay;
    /**
    public float playerHealth = 250;
    float currentHealth;
    public float playerLives = 6;
    public float respawnDelay;
    public float score = 0;
    **/
    int playerHealth, currentHealth;
    int playerLives;
    float score;
    //public GameObject player = null;
    public Character player = null;
    //respawnManager respawnScript;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null){
          player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
          //player = (Character)FindObjectOfType(Character);
        }
        //Debug.Log(player);
        float[] stats = player.getStats();
        playerHealth = (int) stats[0];
        playerLives = (int) stats[1];
        score = stats[2];

        Score.text = "Score: " + score;
        healthDisplay.text = "Health: " + playerHealth;
        livesDisplay.text = "Lives: " + playerLives;
        //Lives = GetComponent<Text>();
        //currentHealth = playerHealth;
        //respawnScript = gameObject.GetComponent<respawnManager>();

    }

    // Update is called once per frame
    void Update()
    {
      float[] stats = player.getStats();
      playerHealth = (int) stats[0];
      playerLives = (int) stats[1];
      score = stats[2];

      Score.text = "Score: " + score;
      healthDisplay.text = "Health: " + playerHealth;
      livesDisplay.text = "Lives: " + playerLives;
    }

}
