using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peon : enemyBehavior{

    bool right = true;
    private float timeBtwShots;
    public float maxDistance;
    float minDistance;
    float currentDistance;


    void Start(){
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().transform;
        playerProfile = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        health = 50;
        minDistance = gameObject.transform.position.x;
        maxDistance = maxDistance + minDistance;
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * travelSpeed;
        timeBtwShots = 0;
    }

    void Update(){
        float distance = Mathf.Abs(transform.position.x - Player.position.x);
        float ydistance = Mathf.Abs(transform.position.y - Player.position.y);


        currentDistance = gameObject.transform.position.x;
        if (currentDistance >= maxDistance && right == true)
        {
            flip();
        }
        if (currentDistance <= minDistance && right == false)
        {
            flip();
        }


        if (distance <= MaxDist  && ydistance<=1) {
            
            if (Player.position.x < transform.position.x) {
              transform.localEulerAngles = new Vector3 (0f, 180f, 0f);
            } else {
              transform.localEulerAngles = new Vector3 (0f, 0f, 0f);

            }
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * 0;
            transform.position = this.transform.position;
            if (distance <= MinDist)
            {
                CheckIfTimeToFire();

            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * travelSpeed;
        }
    }

    public void flip()
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

    void CheckIfTimeToFire() {
      if (timeBtwShots <= 0 && playerProfile.playerHealth>0) {
            bulletObject.layer = 8;
            Instantiate(bulletObject, spawnLocation.position, spawnLocation.rotation);
            timeBtwShots = startTimeBtwShots;
            if (playerProfile.playerHealth <= 0)
            {
                Debug.Log("killed"); 
                timeBtwShots = startTimeBtwShots+2f;
            }

        }
      else {
            if (playerProfile.playerHealth > 0)
            {
                timeBtwShots -= Time.deltaTime;
            }
      }
    }
}
