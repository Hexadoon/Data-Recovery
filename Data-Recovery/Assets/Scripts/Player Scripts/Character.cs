using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public LayerMask playerMask;
    scoreboard scoreManager;

    public followPlayer cam_follow;
    public boss boss;
    //movement
    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    bool grounded = false;
    bool pointRight = true;

    public bool bossKilled = false;
    bool at_boss = false;

    //combat
    public int playerHealth;
    int defaultHealth = 25;
    public float playerLives;
    public float score = 0;
    public Transform bulletSpawn;
    public GameObject bulletObject;


    //respawn
    Transform currentRespawnLocation;
    public Transform initialLocation;
    public float respawnDelay;

    public HealthBar healthBar;
    public EndMenu end;
    public PauseMenu pause;

    //Animations
    Animator playerAnimations;

    bool paused;

    // Start is called before the first frame update
    void Start()
    {

        currentRespawnLocation = initialLocation;
        defaultHealth = playerHealth;
        playerAnimations = gameObject.GetComponent<Animator>();
        scoreManager = gameObject.GetComponent<scoreboard>();
        
        //end = gameObject.GetComponent<EndMenu>();
        //end = (EndMenu)FindObjectOfType(typeof(EndMenu));


    }

    // Update is called once per frame
    void Update(){
      if (playerLives < 1 /* ||  player has reached the end */) {
          end.EndGame(false);
      }
        float movementValue = Input.GetAxis("Horizontal");
        if(pointRight && movementValue < 0){
            flip();
        }
        if (!pointRight && movementValue > 0)
        {
            flip();
        }
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 horizontalMovement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        if (horizontal != 0)
        {
            playerAnimations.SetBool("runPlayer", true);

            playerAnimations.SetFloat("runSpeed", Mathf.Abs(Mathf.Pow(horizontal,2)));
        }
        else
        {
            playerAnimations.SetBool("runPlayer", false);
            playerAnimations.SetFloat("runSpeed", 0);
        }
        transform.position += horizontalMovement * Time.deltaTime * movementSpeed;
        if (Input.GetButtonDown("Jump") && grounded){
            playerAnimations.SetBool("runPlayer", false);
            playerAnimations.SetFloat("runSpeed", 0);
            playerAnimations.SetTrigger("jumpPlayer");

            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);

        }

        if (Input.GetButtonDown("Fire1") && !PauseMenu.GameIsPaused)
        {
            bulletObject.layer = 9;
            playerAnimations.SetTrigger("aimPlayer");
            Instantiate(bulletObject, bulletSpawn.position, bulletSpawn.rotation);

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            //Debug.Log("Not Grounded");
            grounded = false;
            playerAnimations.SetBool("exitJump", false);


        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Ground")
        {
            //Debug.Log("Grounded");

            grounded = true;
            playerAnimations.SetBool("exitJump",true);

        }

        if(collision.tag == "Finish")
        {
            Debug.Log("finished");
            end.EndGame(true);
        }

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
            getHitByenemy(defaultHealth);
        }

        if(collision.tag == "Bullet")
        {
            if (collision.gameObject.layer != 9)
            {

                bulletMachanics projectileScript = collision.GetComponent<bulletMachanics>();
                float damageOccured = projectileScript.damage;
                Destroy(collision.gameObject);
                getHitByenemy(damageOccured);

            }

        }
        // might want to smooth these transitions
        if (collision.name == "BossBegin") {
          at_boss = true;
          healthBar.enableBar();
        }
        if (collision.name == "BossEnd" && !boss.isAlive()/* and check if boss is alive*/) {
          at_boss = false;
          healthBar.disableBar();
        }
    }



    private void flip(){
        pointRight = !pointRight;
        transform.Rotate(0f, 180f, 0f);
    }


    void getHitByenemy(float damage)

    {
        playerHealth -=(int) damage;
        Debug.Log(playerHealth);
        scoreManager.updateHealth();
        if (playerHealth <= 0)
        {
            gameObject.SetActive(false);
            playerLives--;
            scoreManager.updateLives();
            Invoke("respawnPlayer", 2f);
        }
    }

    public void getKill()
    {
        score++;
        scoreManager.updateScore();

    }
    void respawnPlayer()
    {
        if (at_boss)
        {
            at_boss = false;
           // cam_follow.switchCamera(at_boss);
            healthBar.disableBar();
        }
        Debug.Log("playerLives = " + playerLives);
        transform.position = currentRespawnLocation.position;
        playerHealth = defaultHealth;
        scoreManager.updateHealth();
        cam_follow.lockatBoss = false;
        cam_follow.disableCollider();
        gameObject.SetActive(true);
        //bossEnter.isKinematic = true;
        //bossExit.isKinematic = true;

    }

}
