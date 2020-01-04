using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    // public CharacterController2D controller;
    public PlatformEffector2D platforms;

    float runSpeed = 40f;
    float jumpForce = 700f;
    float movementSmoothing = .05f;
    float hMove = 0f; 
    float yMove = 0f; 
    bool jumping = false;
    bool runningJumpOff = false;
    int playerLayer, platformLayer;
    Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.NameToLayer ("Player");
		platformLayer = LayerMask.NameToLayer ("Platform");
        Debug.Log(playerLayer);
        Debug.Log(platformLayer);
    }

    public void OnMove(InputValue input) {
        hMove = input.Get<float>();
    }

    public void OnJump() {
        jumping = true;
    }

    public void OnCrouch() {
        // TODO: Check if is in
        if (!runningJumpOff && rb.velocity.y == 0) {
            StartCoroutine ("JumpOff");
        }
    }

    void FixedUpdate() {
        float move = hMove * runSpeed * Time.fixedDeltaTime;
        Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (jumping && rb.velocity.y == 0) {
            rb.AddForce(new Vector2(0f, jumpForce));
        }

        jumping = false;
    }

	IEnumerator JumpOff()
	{
		runningJumpOff = true;
		platforms.rotationalOffset = 180;
        platforms.surfaceArc = 0;
        yield return new WaitForSeconds (0.4f);
		platforms.rotationalOffset = 0;
        platforms.surfaceArc = 180;
		runningJumpOff = false;
	}
}
