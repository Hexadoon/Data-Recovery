using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public LayerMask playerMask;

    public followPlayer cam_follow;
    public boss boss;
    //movement
    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    bool grounded = false;
    bool pointRight = true;

    bool at_boss = false;

    //combat
    public float playerHealth = 250;
    float currentHealth;
    public float playerLives;
    public float score = 0;
    public Transform bulletSpawn;
    public GameObject bulletObject;


    //respawn
    Transform currentRespawnLocation;
    public Transform initialLocation;
    public float respawnDelay;

    //other
    public HealthBar healthBar;
    //public Rigidbody2D bossEnter;
    //public Rigidbody2D bossExit;

    // Start is called before the first frame update
    void Start()
    {
      currentRespawnLocation = initialLocation;
      currentHealth = playerHealth;
      /**
      bossEnter = GetComponent<Rigidbody2D>();
      bossEnter.isKinematic = true;
      //bossEnter.detectionCollisions = false;
      bossExit = GetComponent<Rigidbody2D>();
      bossExit.isKinematic = true;
      //bossExit.detectionCollisions = false;
**/
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

        if (Input.GetButtonDown("Fire1"))
        {
            bulletObject.layer = 9;
            Instantiate(bulletObject, bulletSpawn.position, bulletSpawn.rotation);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
      Debug.Log(collision.collider.name);
        if(collision.collider.tag == "Ground"){
            grounded = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
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
            if (collision.gameObject.layer != 9){
              bulletMachanics projectileScript = collision.GetComponent<bulletMachanics>();
              float damageOccured = projectileScript.damage;
              currentHealth -= damageOccured;
              Destroy(collision.gameObject);
              if (currentHealth <= 0)
              {
                playerLives--;
                Invoke("respawnPlayer", 2f);
              }
            }
        }
        // might want to smooth these transitions
        if (collision.name == "BossBegin") {
          at_boss = true;
          cam_follow.switchCamera(at_boss);
          healthBar.enableBar();
          //bossEnter.isKinematic = false;
          //bossExit.isKinematic = false;
        }
        if (collision.name == "BossEnd" && !boss.isAlive()/* and check if boss is alive*/) {
          at_boss = false;
          cam_follow.switchCamera(at_boss);
          healthBar.disableBar();
          //bossEnter.isKinematic = true;
          //bossExit.isKinematic = true;
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
    }
    void respawnPlayer()
    {
        at_boss = false;
        cam_follow.switchCamera(at_boss);
        healthBar.disableBar();
        Debug.Log("playerLives = " + playerLives);
        transform.position = currentRespawnLocation.position;
        currentHealth = playerHealth;
        //bossEnter.isKinematic = true;
        //bossExit.isKinematic = true;

    }

    public bool atBoss() {
      return at_boss;
    }
}
