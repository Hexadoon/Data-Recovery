﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMachanics : MonoBehaviour
{

    public float travelSpeed = 20f;
    public float damage = 50f;
    //float delayTimer = 0.2f;

    void Start(){
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * travelSpeed;
        //SDestroy(this, destroyTimer);
        Destroy(gameObject, 0.9f);
    }

    void Update(){

    }

    /**
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Respawn")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right*0f;
            //add bullet impact animation here
            Invoke("destoryObject", delayTimer);
        }
    }
    **/

    void destoryObject()
    {
        Destroy(gameObject);
    }
}
