using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public LayerMask playerMask;

    //movement
    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    bool grounded = false;
    bool pointRight = true;


    //combat
    public float playerHealth = 250;
    float currentHealth;
    public float playerLives;
    //public float respawnDelay;
    public float score = 0;
    //respawnManager respawnScript;


    //respawn
    Transform currentRespawnLocation;
    public Transform initialLocation;
    public float respawnDelay;

    // Start is called before the first frame update
    void Start()
    {
      //respawnScript = gameObject.GetComponent<respawnManager>();
      currentRespawnLocation = initialLocation;
      currentHealth = playerHealth;

    }

    // Update is called once per frame
    void Update(){
        float movementValue = Input.GetAxis("Horizontal");
        if(pointRight && movementValue < 0){
            flip();
        }
        if (!pointRight && movementValue > 0)
        {
            flip();
        }
        Vector3 horizontalMovement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += horizontalMovement * Time.deltaTime * movementSpeed;
        if (Input.GetButtonDown("Jump") && grounded)
            {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){

        if(collision.collider.tag == "Ground"){
            grounded = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      //Debug.Log(collision.tag);
        if (collision.tag == "Respawn")
        {
            currentRespawnLocation = collision.transform;
        }

        if (collision.tag == "deathTrap")
        {
            Invoke("respawnPlayer", respawnDelay);
            playerLives--;
        }
        if(collision.tag == "Enemy")
        {
            getHitByenemy();
        }

        if(collision.tag == "Bullet")
        {
            //Debug.Log(collision.gameObject.layer);
            if (collision.gameObject.layer != 9){
              //Debug.Log("Bullet Hit");
              bulletMachanics projectileScript = collision.GetComponent<bulletMachanics>();
              float damageOccured = projectileScript.damage;
              //Debug.Log(projectileScript.layer);
              currentHealth -= damageOccured;
              Destroy(collision.gameObject);
              if (currentHealth <= 0)
              {
                playerLives--;
                Invoke("respawnPlayer", 2f);
              }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if (collision.collider.tag == "Ground"){
            grounded = false;
        }
    }

    private void flip(){
        pointRight = !pointRight;
        transform.Rotate(0f, 180f, 0f);
    }
    public float[] getStats() {
      float[] stat = {currentHealth, playerLives, score};
      /**
      stat[0] = playerHealth;
      stat[1] = playerLives;
      stat[2] = Score;
      **/
      return stat;
    }
    void getHitByenemy()
    {
        currentHealth -= 300;
        if (currentHealth <= 0)
        {
            playerLives--;
            Invoke("respawnPlayer", 2f);
        }
        //healthDisplay.text = "Health: " + playerHealth;
    }
    void respawnPlayer()
    {
        Debug.Log("playerLives = " + playerLives);
        //respawnScript.respawnPlayer();
        transform.position = currentRespawnLocation.position;
        currentHealth = playerHealth;

    }
}
