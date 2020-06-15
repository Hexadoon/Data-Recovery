using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnManager : MonoBehaviour
{
    Transform currentRespawnLocation;
    public Transform initalLocation;
    public float respawnDelay;
    void Start(){
        currentRespawnLocation = initalLocation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            currentRespawnLocation = collision.transform;
        }

        

        if (collision.tag == "deathTrap")
        {
            Invoke("respawnPlayer", respawnDelay);
        }
    }

    public void respawnPlayer()
    {
        Debug.Log("Respawn");
        transform.position = currentRespawnLocation.position;
    }

}
