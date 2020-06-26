using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : enemyBehavior
{
    bool goingUp = true;
    public float maxHeight;
    float minHeight;
    float currentHealth;


    public float startTimeBtwJumps = 5f;
    public float startTimeBtwRush = 5f;
    private float timeBtwShots;
    private float timeBtwJumps;
    private float timeBtwRush;

    public float chargeSpeed;
    //public float health = 50;
    public HealthBar healthBar;
    public BoxCollider2D barraier;


    public float dropForce = 5f;
    int ChaseDistance = 20;

    public float jumpForce = 5f;
    bool isGrounded = true;

    Rigidbody2D rb;
    Character player;


    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().transform;
        health = 100;
        currentHealth = health;
        healthBar.SetMaxHealth(health);
        minHeight = gameObject.transform.position.y;
        maxHeight = maxHeight + minHeight;

        rb = gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * travelSpeed;
        timeBtwShots = startTimeBtwShots;
        timeBtwJumps = startTimeBtwJumps;
        timeBtwRush = startTimeBtwRush;

    }



    void Update(){
        float distance = Vector3.Distance(transform.position, Player.position);

        /**
        currentHeight = gameObject.transform.position.y + minHeight;
        if (currentHeight >= maxHeight && goingUp == true )
        {
            flip();
        }
        if (currentHeight <= minHeight && goingUp == false)
        {
            flip();
        }
        **/

        if (currentHealth < .5 * health) {
          travelSpeed = 5f;
          startTimeBtwShots = 2.5f;

        }

        // if player is within first range, chase after player

        if (distance <= ChaseDistance && distance >= MaxDist) {
          if (Player.position.x < transform.position.x) {
            transform.localEulerAngles = new Vector3 (0f, 180f, 0f);
            rb.velocity = transform.right * travelSpeed;
          } else {
            transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
            rb.velocity = -transform.right * travelSpeed;

          }
          //travelSpeed = 2f;
          //transform.position = Vector2.MoveTowards(transform.position, Player.position, travelSpeed);


        }
        else if (distance >= MinDist && distance <= MaxDist) {
            CheckIfTimeToRush();
            if (Player.position.x < transform.position.x) {
              transform.localEulerAngles = new Vector3 (0f, 180f, 0f);
              rb.velocity = transform.right * travelSpeed;
            } else {
              transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
              rb.velocity = -transform.right * travelSpeed;

            }
            //gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * 0;
            //transform.position = this.transform.position;
            //CheckIfTimeToJump();

            travelSpeed = 0f;
            //StartCoroutine(JumpLogic());
        }


        // if player is within second range, shoot the player
        if (distance <= MinDist) {
          //transform.LookAt(Player);
          CheckIfTimeToFire();

        }
    }

    private void OnCollisionEnter2D(Collision2D collision){

        if(collision.collider.tag == "Ground"){
            isGrounded = true;
            //rb.gravityScale = 0;

        }

    }


    public void flip()
    {
        goingUp = !goingUp;
        if (goingUp == true)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * travelSpeed;
        }

        if (goingUp == false)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * -travelSpeed;
        }
    }
    void CheckIfTimeToFire() {
      if (timeBtwShots <= 0) {
        bulletObject.layer = 8;
        Instantiate(bulletObject, spawnLocation.position, spawnLocation.rotation);
        timeBtwShots = startTimeBtwShots;
        //nextFire = Time.time + fireRate;
      }
      else {
        timeBtwShots -= Time.deltaTime;
      }
    }

    void CheckIfTimeToJump() {
      if (timeBtwJumps <= 0 && isGrounded) {
        jump();
        rb.AddForce(Vector2.down * dropForce, ForceMode2D.Impulse);
        //isGrounded = true;
        timeBtwJumps = startTimeBtwJumps;
      }
      else {
        timeBtwJumps -= Time.deltaTime;
      }
    }

    void CheckIfTimeToRush() {
      if (timeBtwRush <= 0) {
        chargeSpeed = 1f;
        transform.position = Vector2.MoveTowards(transform.position, Player.position, chargeSpeed);
        timeBtwRush = startTimeBtwRush;
      }
      else {
        timeBtwRush -= Time.deltaTime;
      }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            Debug.Log(collision.gameObject.layer);
            if (collision.gameObject.layer != 8){
              Debug.Log("Bullet Hit");
              bulletMachanics projectileScript = collision.GetComponent<bulletMachanics>();
              float damageOccured = projectileScript.damage;
              //Debug.Log(projectileScript.layer);
              currentHealth -= damageOccured;
              Destroy(collision.gameObject);
              healthBar.SetHealth(currentHealth);
              if (currentHealth <= 0)
              {
                  Debug.Log("ded");
                  Destroy(gameObject);
                  healthBar.disableBar();
              }
            }
        }
        if (collision.name == "BossBegin") {
            transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
            rb.velocity = -transform.right * travelSpeed;
            Debug.Log("bossbeginblah");
        }
        if (collision.name == "BossEnd" ) {
          transform.localEulerAngles = new Vector3 (0f, 180f, 0f);
          rb.velocity = transform.right * travelSpeed;
          Debug.Log("bossendblah");
        }
    }
    public bool isAlive() {
      return currentHealth > 0;
    }

    void jump() {
      Debug.Log("i do the jumpjump");
      rb.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
      isGrounded = !isGrounded;

      /**
      rb.velocity = Vector2.zero;
      rb.angularVelocity = 0;
      rb.gravityScale = 0;
      **/
    }

    IEnumerator JumpLogic()
  {
    float minWaitTime = 5f;
    float maxWaitTime = 10f;

    yield return new WaitForSeconds(startTimeBtwJumps);
    //if (currentHealth <= 0) return;
    jump();
  }

}
