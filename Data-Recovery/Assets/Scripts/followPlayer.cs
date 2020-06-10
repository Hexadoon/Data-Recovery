using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform player;
    public Vector2 offset;
    public float mapStartingPoint;
    public float mapEndPoint;

    void LateUpdate()
    {
        float playerX = player.position.x;

        if (playerX > mapStartingPoint && playerX < mapEndPoint){
            transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
        }
        
    }
}
