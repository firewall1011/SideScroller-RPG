using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;

    private Animator animator;
    
    private float runSpeed = 40f;
    private float hMove = 0f; 
    private float yMove = 0f; 
    private bool jumping = false;
    private bool crouching = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue input)
    {
        hMove = input.Get<float>();
    }

    public void OnJump()
    {
        jumping = true;
        animator.SetBool("isJumping", true);
    }

    public void OnLand()
    {
        animator.SetBool("isJumping", false);
        Debug.Log("Landing");
    }

    public void OnCrouch()
    {
        crouching = true;
    }

    void FixedUpdate() {
        float currentSpeed = hMove * runSpeed * Time.fixedDeltaTime;
        controller.Move(currentSpeed, false, jumping);
        animator.SetFloat("Speed", Mathf.Abs(currentSpeed));
        jumping = false;
        crouching = false;
    }
}
