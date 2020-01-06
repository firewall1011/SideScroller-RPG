using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    public MovementController controller;

    float hMove = 0f;

    bool jumping = false;
    bool jumpingOff = false;
    

    // Rigidbody2D rb;


    void Start() {
        // rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue input) {
        hMove = input.Get<float>();
    }

    public void OnJump() {
        jumping = true;
    }

    // public void OnLand()
    // {
    //     onJumpEvent?.Invoke(false);
    // }

    public void OnCrouch() {
        jumpingOff = true;
    }

    void FixedUpdate() {
        // Flip();  
        
        controller.Move(hMove, jumping, jumpingOff);      

        // onMoveEvent?.Invoke(Mathf.Abs(rb.velocity.x));

        // if (rb.velocity.y == 0)
        //     onJumpEvent?.Invoke(false);
        // if (jumping && rb.velocity.y == 0) {
        //     onJumpEvent?.Invoke(true);
        // }
    

        jumping = false;
        jumpingOff = false;

    }



    
}
