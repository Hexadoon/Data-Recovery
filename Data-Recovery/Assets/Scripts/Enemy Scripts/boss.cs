using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : enemyBehavior
{
    bool goingUp = true;
    public float maxHeight;
    float minHeight;
    float currentHeight;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().transform;
        health = 100;
        minHeight = gameObject.transform.position.y;
        maxHeight = maxHeight + minHeight;

        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * travelSpeed;

    }


    void Update()
    {
        currentHeight = gameObject.transform.position.y + minHeight;
        if (currentHeight >= maxHeight && goingUp == true )
        {
            flip();
        }
        if (currentHeight <= minHeight && goingUp == false)
        {
            flip();
        }
    }

/**
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Bullet" && collision.gameObject.layer != 8)
        {
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
}
