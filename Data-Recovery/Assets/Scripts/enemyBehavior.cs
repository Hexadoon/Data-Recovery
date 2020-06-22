using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class enemyBehavior : MonoBehaviour
{
    public Transform Player;
    public float health;
    public float travelSpeed;

    public int MaxDist = 25;
    public int MinDist = 20;



    //public float fireRate;
    //public float nextFire;

    //public float timeBtwShots;
    public float startTimeBtwShots;

    public Transform spawnLocation;
    public GameObject bulletObject;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().transform;
        Debug.Log(Player);
    }

    // Update is called once per frame
    /**
    void Update()
    {
        transform.LookAt(Player);

        float distance = Vector3.Distance(transform.position, Player.position);
        //Debug.Log("distance");
        //Debug.Log(distance);
        if (Vector3.Distance(transform.position, Player.position) >= MinDist) {
          transform.position += transform.forward * travelSpeed;
        }
        if (Vector3.Distance(transform.position, Player.position) <= MaxDist) {
          // here call any function; like shooting at player
          //Debug.Log("distance= " + distance);
          //Instantiate(bulletObject, spawnLocation.position, spawnLocation.rotation);
        }
    }
    **/

    void Update() {

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Respawn")
        {
            flip();
        }
        /**
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
        **/
        if(collision.tag == "Bullet")
        {
            Debug.Log(collision.gameObject.layer);
            if (collision.gameObject.layer != 8){
              Debug.Log("Bullet Hit");
              bulletMachanics projectileScript = collision.GetComponent<bulletMachanics>();
              float damageOccured = projectileScript.damage;
              //Debug.Log(projectileScript.layer);
              health -= damageOccured;
              Destroy(collision.gameObject);
              if (health <= 0)
              {
                  Debug.Log("ded");
                  Destroy(gameObject);
              }
            }
        }
    }

    public abstract void flip() ;

}
