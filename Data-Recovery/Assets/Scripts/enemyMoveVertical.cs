using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMoveVertical : MonoBehaviour
{
    public float travelSpeed;
    bool goingUp = true;
    public float health = 100;
    public float maxHeight;
    float minHeight;
    float currentHeight;
    void Start()
    {
        minHeight = gameObject.transform.position.y;
        maxHeight = maxHeight + minHeight;

        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * travelSpeed;

    }

    void Update()
    {
        currentHeight = gameObject.transform.position.y - minHeight;
        if (currentHeight >= maxHeight && goingUp == true )
        {
            flip();
        }
        if (currentHeight <= minHeight && goingUp == false)
        {
            flip();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Bullet")
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
    private void flip()
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
