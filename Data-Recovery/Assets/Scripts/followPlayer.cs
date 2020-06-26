using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Camera main_cam;
    public Transform player;
    public Vector2 offset;
    public float mapStartingPoint;
    public float mapEndPoint;

    public float bossBeginPoint;
    public float bossEndPoint;
    public float bossFixedPoint;
    public bool lockatBoss;

    public GameObject[] bossColliders;


    public float shiftSpeed;
    void Start() {
        lockatBoss = false;
        foreach (GameObject collide in bossColliders)
        {
            collide.SetActive(false);
        }
    }
    void LateUpdate()
    {
        float playerX = player.position.x;

        if (playerX > mapStartingPoint && playerX < mapEndPoint && !lockatBoss){
            transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
            if(transform.position.x >= bossBeginPoint)
            {
                lockatBoss = true;
                foreach (GameObject collide in bossColliders)
                {
                    collide.SetActive(true);
                }
            }
        }
        if (lockatBoss && transform.position.x<bossFixedPoint)
        {
            transform.position = new Vector3(transform.position.x+ (Time.deltaTime *shiftSpeed), transform.position.y, transform.position.z);

        }

        if (playerX <= mapStartingPoint)
        {
            transform.position = new Vector3(mapStartingPoint, transform.position.y, transform.position.z);
        }
        

    }
    
    public void 
   

}
