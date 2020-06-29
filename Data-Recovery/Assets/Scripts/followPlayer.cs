using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Camera main_cam;
    public Transform player;
    public GameObject boss;
    public Vector2 offset;
    public float mapStartingPoint;
    public float mapEndPoint;

    public float bossBeginPoint;
    public float bossEndPoint;
    public float bossFixedPoint;
    public bool lockatBoss;
    public bool bossDefeat;
    bool locked;
    public GameObject[] bossColliders;


    public float shiftSpeed;
    void Start() {
        lockatBoss = false;
        bossDefeat = false;
        locked = false;
        boss.SetActive(false);
        foreach (GameObject collide in bossColliders)
        {
            collide.SetActive(false);
        }
    }
    void LateUpdate()
    {
        float playerX = player.position.x;

        if (playerX > mapStartingPoint && playerX < mapEndPoint && !lockatBoss  && !locked){
            transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
            if(transform.position.x >= bossBeginPoint  && bossDefeat==false)
            {
                lockatBoss = true;
                boss.SetActive(true);
                foreach (GameObject collide in bossColliders)
                {
                    collide.SetActive(true);
                }
            }
        }
        if (lockatBoss && transform.position.x<bossFixedPoint && !locked)
        {
            transform.position = new Vector3(transform.position.x+ (Time.deltaTime *shiftSpeed), transform.position.y, transform.position.z);

        }

        if (playerX <= mapStartingPoint && !locked) 
        {
            transform.position = new Vector3(mapStartingPoint, transform.position.y, transform.position.z);
        }
        if (locked)
        {
            if (transform.position.x > player.position.x)
            {
                transform.position = new Vector3(transform.position.x - (Time.deltaTime * shiftSpeed), transform.position.y, transform.position.z);
            }
            if (transform.position.x < player.position.x)
            {
                transform.position = new Vector3(transform.position.x + (Time.deltaTime * shiftSpeed), transform.position.y, transform.position.z);
            }
            if(transform.position.x <= (player.position.x+.1)  && transform.position.x >= (player.position.x - .1))
            {
                locked = false;
            }
        }

    }

    public void bossDefeated()
    {
        lockatBoss = false;
        bossDefeat = true;
        locked = true;
        foreach(GameObject collide in bossColliders){
            collide.SetActive(false);
        }
        Debug.Log("defeated");

        


    }
    public void disableCollider()
    {
        foreach (GameObject collide in bossColliders)
        {
            collide.SetActive(false);
        }
    }
   

}
