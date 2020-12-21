using System.Collections;
using System;
using UnityEngine;

public class MovementController : MonoBehaviour, IMovable {
    //IMovable
    public event Action onCrouchEvent;
    public event Action<bool> onJumpEvent;
    public event Action<float> onMoveEvent;
    
    public float runSpeed = 40f;
    public float movementSmoothing = 0.05f;
    public float jumpForce = 700f;
    
    private bool runningJumpOff = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D[] platforms;
    
    private float hMove = 0;
    private bool jumping = false;
    private bool jumpingOff = false;

    private float facingDir = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Platform");
        
        platforms = new Collider2D[gos.Length];
        for (int i = 0; i < gos.Length; i++) {
            platforms[i] = gos[i].GetComponent<Collider2D>();
        }
    }
    
    void FixedUpdate() {
        Flip();

        // JUMP OFF
        if (jumpingOff && !runningJumpOff && rb.velocity.y == 0 && platforms != null) {
            StartCoroutine ("JumpOff");
            onCrouchEvent?.Invoke();
        }
        
        // HORIZONTAL MOVE
        Vector3 velocity = Vector3.zero;
        
        float move = hMove * runSpeed * Time.fixedDeltaTime;
        Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
        
        onMoveEvent?.Invoke(Mathf.Abs(rb.velocity.x));

        // JUMP
        if (rb.velocity.y == 0)
            onJumpEvent?.Invoke(false);
        if (jumping && rb.velocity.y == 0) {
            onJumpEvent?.Invoke(true);
            rb.AddForce(new Vector2(0f, jumpForce));
        }
        
        // RESET
        jumping = false;
        jumpingOff = false;
    }
   
    
    public void Move(float movingDir, bool jump, bool jumpOff) {
        hMove = movingDir;
        jumping = jump;
        jumpingOff = jumpOff;
    }
    
    
    IEnumerator JumpOff() {
        runningJumpOff = true;
        
        foreach (Collider2D platform in platforms)
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), platform, true);
        
        yield return new WaitForSeconds (0.4f);

        foreach (Collider2D platform in platforms)
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), platform, false);

        runningJumpOff = false;
    }
    
    void Flip() {
        if (hMove != 0 && facingDir != hMove) {
            facingDir = hMove;
            spriteRenderer.transform.Rotate(0f, 180 * Mathf.Sign(hMove), 0f);
        }
    }
}

