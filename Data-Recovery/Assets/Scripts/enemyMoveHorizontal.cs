using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMoveHorizontal : MonoBehaviour{

    public float travelSpeed;
    bool right = true;
    public float health = 50;
    public float maxDistance;
    float minDistance;
    float currentDistance;
    void Start(){
        minDistance = gameObject.transform.position.x;
        maxDistance = maxDistance + minDistance;

        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * travelSpeed;
    }

    void Update(){
        currentDistance = gameObject.transform.position.x;
        if (currentDistance >= maxDistance && right == true)
        {
            flip();
        }
        if (currentDistance <= minDistance && right == false)
        {
            flip();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Bullet")
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
        right = !right;
        if(right == true)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * travelSpeed;
        }

        if(right == false)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * -travelSpeed;
        }
    }
}
