using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    
    float runSpeed = 40f;
    float hMove = 0f; 
    float yMove = 0f; 
    bool jumping = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        hMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump")) {
            jumping = true;
        }
        
    }

    void FixedUpdate() {
        controller.Move(hMove * runSpeed * Time.fixedDeltaTime, yMove == -1, jumping);
        jumping = false;
    }
}
