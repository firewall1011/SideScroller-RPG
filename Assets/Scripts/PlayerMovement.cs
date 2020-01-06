using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    //IMovable
    public event Action<bool> onJumpEvent;
    public event Action<float> onMoveEvent;
    public event Action onCrouchEvent;

    // public CharacterController2D controller;
    public Collider2D platforms;

    float runSpeed = 40f;
    float jumpForce = 700f;
    float movementSmoothing = .05f;
    float hMove = 0f;
    float facingDir = 1;

    bool jumping = false;
    bool runningJumpOff = false;

    Rigidbody2D rb;

    Vector3 velocity = Vector3.zero;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue input) {
        hMove = input.Get<float>();
    }

    public void OnJump() {
        jumping = true;
    }

    public void OnLand()
    {
        onJumpEvent?.Invoke(false);
    }

    public void OnCrouch() {
        if (!runningJumpOff && rb.velocity.y == 0) {
            StartCoroutine ("JumpOff");
            onCrouchEvent?.Invoke();
        }
    }

    void FixedUpdate() {
        if (hMove != 0 && facingDir != hMove) {
            facingDir = hMove;
            transform.localScale = new Vector3(hMove, 1, 1);
        }


        float move = hMove * runSpeed * Time.fixedDeltaTime;
        Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
        onMoveEvent?.Invoke(Mathf.Abs(rb.velocity.x));

        if (rb.velocity.y == 0)
            onJumpEvent?.Invoke(false);
        if (jumping && rb.velocity.y == 0) {
            rb.AddForce(new Vector2(0f, jumpForce));
            onJumpEvent?.Invoke(true);
        }
    

        jumping = false;

    }

    

    IEnumerator JumpOff() {
        runningJumpOff = true;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), platforms, true);
        yield return new WaitForSeconds (0.4f);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), platforms, false);
        runningJumpOff = false;
    }
}
