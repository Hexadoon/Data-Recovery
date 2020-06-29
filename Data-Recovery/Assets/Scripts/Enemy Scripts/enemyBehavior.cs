using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class enemyBehavior : MonoBehaviour
{
    public Transform Player;
    public Character playerProfile;
    public float health = 5;
    public float travelSpeed;

    public int MaxDist = 25;
    public int MinDist = 20;

    public float startTimeBtwShots;

    public Transform spawnLocation;
    public GameObject bulletObject;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().transform;
        playerProfile = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        Debug.Log(Player);

    }

    // Update is called once per frame


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            if (collision.gameObject.layer == 9){
                bulletMachanics projectileScript = collision.GetComponent<bulletMachanics>();
                float damageOccured = projectileScript.damage;
                health -= damageOccured;
                Debug.Log("Bullet Hit"+health);

                Destroy(collision.gameObject);
                if (health <= 0)
                {
                    playerProfile.getKill();
                    Destroy(gameObject);

                }
            }
        }
    }

}
