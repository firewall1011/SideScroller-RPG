using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour, IAttack, ISkill
{
    public event Action onAttackEvent;
    public event Action<int> onSkill;
    public float AttackDamage => attackDamage;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    public float attackDamage = 1f;
    public GameObject fireballPrefab = null;

    private float nextAttack = 0f;


    public void OnAttack() {
        if (Time.time >= nextAttack) {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in enemies) {
                enemy.GetComponent<IDamagable>().tryHit(attackDamage);
            }
            onAttackEvent?.Invoke();
            
            nextAttack = Time.time + 1f / attackRate;
        }
    }

    public void afterSkillAnimation(int type)
    {
        ProjectileSkill fireball = ProjectileSkill.MakeProjectileSkill(fireballPrefab, transform.position + new Vector3(0, -0.75f, 0f), transform.rotation, transform.localScale);
        fireball.SetSpeed(5f);
        switch (type)
        {
            case 0:
                fireball.onTriggerEnterEvent += (collision) =>
                {
                    if (!collision.CompareTag("Enemy")) return;
                    collision.GetComponent<HealthSystem>()?.tryHit(attackDamage * 2);
                    Destroy(fireball.gameObject);
                };
                break;
            case 1:
                fireball.transform.localScale *= 2;
                fireball.onTriggerEnterEvent += (collision) =>
                {
                    if (!collision.CompareTag("Enemy")) return;
                    collision.GetComponent<HealthSystem>()?.tryHit(attackDamage * 5);
                    Destroy(fireball.gameObject);
                };
                break;
            default:
                break;
        }
        
    }

    public void OnSkill_0() => onSkill?.Invoke(0);
    public void OnSkill_1() => onSkill?.Invoke(1);
    public void OnSkill_2() => onSkill?.Invoke(2);
    public void OnSkill_3() => onSkill?.Invoke(3);

    void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
