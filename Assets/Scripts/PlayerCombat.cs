using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public event System.Action onAttackEvent;
    
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;

    private float nextAttack = 0f;
    
    public void OnAttack() {
        if (Time.time >= nextAttack) {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in enemies) {
                enemy.GetComponent<IDamagable>().tryHit(1f);
            }
            onAttackEvent?.Invoke();
            
            nextAttack = Time.time + 1f / attackRate;
        }
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
