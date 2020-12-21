using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour, IAttack
{
    public event Action onAttackEvent;
    public float AttackDamage => attackDamage;

    public float lookRadius = 5f;
    public LayerMask targetLayers;
    public MovementController controller;

    public float attackRate = 1f;
    public float attackDamage = 1f;

    private float nextAttack;

    void FixedUpdate() {
        Collider2D[] allTargets = Physics2D.OverlapCircleAll(transform.position, lookRadius, targetLayers);

        if (allTargets.Length == 0) 
            controller.Move(0, true, false);

        foreach (Collider2D target in allTargets) {
            Vector3 targetPos = target.transform.position;
            Vector3 pos = transform.position;

            Vector2 dir = targetPos - pos;
            if (Mathf.Abs(dir.x) >= 1.0f)
                dir.x /= Mathf.Abs(dir.x);
            else
                dir.x = 0;
            
            if (Mathf.Abs(dir.y) >= 1.0f) 
                dir.y /= Mathf.Abs(dir.y);
            else
                dir.y = 0;

            Debug.Log("x: " + dir.x);
            // Debug.Log("y: " + dirY);
            
            controller.Move(dir.x, dir.y > 0f, dir.y < 0f);

            if (dir.x == 0 && dir.y == 0 && Time.time > nextAttack) {

                target.GetComponent<IDamagable>().tryHit(attackDamage);
                nextAttack = Time.time + 1f / attackRate;
                onAttackEvent?.Invoke();
            }
        }
    }    

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
