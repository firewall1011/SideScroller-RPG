using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public event System.Action onAttackEvent;
    
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    
    public void OnAttack() {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in enemies) {
            enemy.GetComponent<IDamagable>().tryHit(1f);
        }
        onAttackEvent?.Invoke();
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
