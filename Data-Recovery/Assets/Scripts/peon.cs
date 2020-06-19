using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peon : enemyBehavior{

    //public float travelSpeed;
    bool right = true;
    private float timeBtwShots;
    //public float health = 50;


    void Start(){
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().transform;
        health = 50;
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * travelSpeed;
        timeBtwShots = startTimeBtwShots;
        //fireRate = 5f;
        //nextFire = Time.time;

    }

    void Update(){
        float distance = Vector3.Distance(transform.position, Player.position);

        //idly move back and forth if player is too far
        if (distance >= MaxDist) {
          gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * travelSpeed;

          //Debug.Log("distance= " + distance);
        }

        // if player is within first range, chase after player

        else if (distance >= MinDist && distance <= MaxDist) {
            //transform.LookAt(Player);
            //transform.rotation =  Quaternion.LookRotation((Player.position - transform.position).normalized , Vector3.up);
            //transform.LookAt(Player.transform.position);
            //transform.Rotate(transform.right);
            //transform.position = Vector2.MoveTowards(transform.position, Player.position, 1 * Time.deltaTime);

            //gameObject.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
            if (Player.position.x < transform.position.x) {
              transform.localEulerAngles = new Vector3 (0f, 180f, 0f);
            } else {
              transform.localEulerAngles = new Vector3 (0f, 0f, 0f);

            }
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * 0;
            transform.position = this.transform.position;
        }


        // if player is within second range, shoot the player
        if (distance <= MinDist) {
          //transform.LookAt(Player);
          CheckIfTimeToFire();

        }
    }

    public override void flip()
    {
        right = !right;
        if(right == true)
        {
            transform.Rotate(0f, -180f, 0f);
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * travelSpeed;
        }

        if(right == false)
        {
            transform.Rotate(0f, 180f, 0f);
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * travelSpeed;
        }
    }

/**
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Respawn")
        {
            flip();
        }
        if(collision.tag == "Bullet" && collision.gameObject.layer != 8)
        {
            Debug.Log(collision.gameObject.layer);
            Debug.Log("Bullet Hit");
            bulletMachanics projectileScript = collision.GetComponent<bulletMachanics>();
            float damageOccured = projectileScript.damage;
            health -= damageOccured;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
**/

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
}
