using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossFight : enemyBehavior
{

    bool popin = true;
    public float popInDistance;

    public followPlayer endfight;
    bool pointRight;
    bool moving;

    public HealthBar healthStatus;
    public GameObject bar;
    // Start is called before the first frame update
    void Start()
    {
        pointRight = false;
        moving = false;
        healthStatus.SetMaxHealth(health);
        healthStatus.SetHealth(health);

    }



    // Update is called once per frame
    void Update()
    {
        if (popin)
        {
            transform.position = new Vector3(transform.position.x - (Time.deltaTime * travelSpeed), transform.position.y, transform.position.z);
            if(transform.position.x <= popInDistance)
            {
                popin = false;
            }
            Debug.Log(transform.position.x);
        }

        float random = Random.Range(0f, 100f);

        if (random <=20 && !moving)
        {
            moving = true;
            Debug.Log("dash");

            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * 10f;
            Invoke("dash", 7);
        }

        if (random >= 75 && !moving)
        {
            moving = true;
            Debug.Log("jump");
            jump();
        }
        if(random>35 && random <75 && !moving)
        {
            shoot();           
        }


    }

    private void dash()
    {
        pointRight = !pointRight;
        transform.Rotate(0f, 180f, 0f);
        moving = false;
    }

    private void jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
        Invoke("shoot", 4);
        Invoke("shoot", 7);
        Invoke("shoot", 10);
        moving = false;
    }

    private void shoot()
    {
        bulletObject.layer = 8;
        Debug.Log("shoot");
        Instantiate(bulletObject, spawnLocation.position, spawnLocation.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (collision.gameObject.layer == 9)
            {
                bulletMachanics projectileScript = collision.GetComponent<bulletMachanics>();
                float damageOccured = projectileScript.damage;
                health -= damageOccured;
                healthStatus.SetHealth(health);

                Debug.Log("Bullet Hit" + health);

                Destroy(collision.gameObject);
                if (health <= 0)
                {
                    Destroy(gameObject);
                    endfight.bossDefeated();
                    healthStatus.disableBar();

                }
            }
        }
    }
}
