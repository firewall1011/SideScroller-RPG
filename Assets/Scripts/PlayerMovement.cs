using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    
    float runSpeed = 40f;
    float hMove = 0f; 
    float yMove = 0f; 
    bool jumping = false;
    bool crouching = false;

    public void OnMove(InputValue input)
    {
        hMove = input.Get<float>();
    }

    public void OnJump()
    {
        jumping = true;
    }

    public void OnCrouch()
    {
        crouching = true;
    }

    void FixedUpdate() {
        controller.Move(hMove * runSpeed * Time.fixedDeltaTime, false, jumping);
        jumping = false;
        crouching = false;
    }
}
