using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform spawnLocation;
    public GameObject bulletObject;

    private void Start(){
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bulletObject, spawnLocation.position, spawnLocation.rotation);
        }
    }
    
}

