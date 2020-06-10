using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMachanics : MonoBehaviour
{
    public float travelSpeed = 20f;
    public float destroyTimer = 4f;
   
    void Start(){
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * travelSpeed;
        Destroy(this, destroyTimer);
    }

    void Update(){
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        if(hitInfo.tag != "Respawn")
        {
            Destroy(gameObject);

        }
    }
}
