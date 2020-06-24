using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreboard : MonoBehaviour
{

    public Text Score;
    public Text healthDisplay;
    public Text livesDisplay;

    float health;
    float lives;
    float score;
    Character player;


    // Start is called before the first frame update
    void Start(){

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        health = player.playerHealth;
        lives = player.playerLives;
        score = player.score;

        Score.text = "Score: " + score;
        healthDisplay.text = "Health: " + health;
        livesDisplay.text = "Lives: " + lives;

    }

    // Update is called once per frame
    public void updateHealth()
    {
        health = player.playerHealth;
        healthDisplay.text = "Health: " + health;
    }

    public void updateLives()
    {
        lives = player.playerLives;
        livesDisplay.text = "Lives: " + lives;
    }

    public void updateScore()
    {
        score = player.score;
        Score.text = "Score: " + score;
    }

}
