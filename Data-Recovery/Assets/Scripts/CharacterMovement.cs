﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    // Start is called before the first frame update
    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    bool grounded = false;
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * movementSpeed;
        if (Input.GetButtonDown("Jump") && grounded)
            {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == "Ground"){
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision){
        if (collision.collider.tag == "Ground")
        {
            grounded = false;
        }
    }
}
