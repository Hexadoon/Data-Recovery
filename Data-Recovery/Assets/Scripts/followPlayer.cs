using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Camera main_cam;
    public Camera boss_cam;
    public Transform player;
    public Vector2 offset;
    public float mapStartingPoint;
    public float mapEndPoint;


    void Start() {
      //main_cam = Camera.main;
      Debug.Log(main_cam);
      main_cam.enabled = true;
      boss_cam.enabled = false;
    }
    void LateUpdate()
    {
        float playerX = player.position.x;
        //Debug.Log(main_cam.enabled);
        if (main_cam.enabled & !boss_cam.enabled) {

          if (playerX > mapStartingPoint && playerX < mapEndPoint){
              transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
          }

          if (playerX <= mapStartingPoint)
          {
              transform.position = new Vector3(mapStartingPoint, transform.position.y, transform.position.z);
          }
        }

    }
    void Update() {
      if (Input.GetKeyDown(KeyCode.L)){
          Debug.Log("L pressed");
          boss_cam.enabled = !boss_cam.enabled;
          main_cam.enabled = !main_cam.enabled;
        }

    }

    public void switchCamera(bool at_boss) {
      if (at_boss) {
        boss_cam.enabled = true;
        main_cam.enabled = false;
      } else {
        main_cam.enabled = true;
        boss_cam.enabled = false;

      }

    }
}
