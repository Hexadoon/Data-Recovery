using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMoveHorizontal : MonoBehaviour{

    public float travelSpeed;
    bool right = true;
    public float health = 50;
    void Start(){
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * travelSpeed;

    }

    void Update(){
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Respawn")
        {
            flip();
        }
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
            transform.Rotate(0f, -180f, 0f);
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * travelSpeed;
        }

        if(right == false)
        {
            transform.Rotate(0f, 180f, 0f);
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * travelSpeed;
        }
    }
}
