using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    public MovementController controller;

    float hMove = 0f;

    bool jumping = false;
    bool jumpingOff = false;
    

    public void OnMove(InputValue input) {
        hMove = input.Get<float>();
    }

    public void OnJump() {
        jumping = true;
    }

    public void OnCrouch() {
        jumpingOff = true;
    }

    void FixedUpdate() {
        controller.Move(hMove, jumping, jumpingOff);

        jumping = false;
        jumpingOff = false;

    }



    
}
