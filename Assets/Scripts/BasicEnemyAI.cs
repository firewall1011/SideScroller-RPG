using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{

    public float lookRadius = 5f;
    public LayerMask targetLayers;
    public MovementController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate() {
        Collider2D[] allTargets = Physics2D.OverlapCircleAll(transform.position, lookRadius, targetLayers);

        if (allTargets.Length == 0) 
            controller.Move(0, true, false);

        foreach (Collider2D target in allTargets) {
            Vector3 targetPos = target.transform.position;
            Vector3 pos = transform.position;
    
            float dirX = (targetPos.x - pos.x);
            if (Mathf.Abs(dirX) >= 1.0f)
                dirX /= Mathf.Abs(dirX);
            else
                dirX = 0;

            float dirY = (targetPos.y - pos.y);

            if (Mathf.Abs(dirY) >= 1.0f) 
                dirY /= Mathf.Abs(dirY);
            else
                dirY = 0;

            // Debug.Log("x: " + dirX);
            // Debug.Log("y: " + dirY);
            
            controller.Move(dirX, dirY > 0, dirY < 0);
        }
    }    

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
