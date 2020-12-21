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
            onAttackEvent?.Invoke();
            
            nextAttack = Time.time + 1f / attackRate;
        }
    }

    public void afterSkillAnimation(int type)
    {
        switch (type)
        {
            case 0:
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in enemies)
                {
                    enemy.GetComponent<IDamagable>().tryHit(attackDamage);
                }
                break;
            case 1:
                ProjectileSkill fireball = ProjectileSkill.MakeProjectileSkill(fireballPrefab, transform.position + new Vector3(0, -0.75f, 0f), attackPoint.transform.rotation, attackPoint.transform.localScale);
                fireball.SetSpeed(5f*Mathf.Sign(attackPoint.rotation.y));
                fireball.onTriggerEnterEvent += (collision) =>
                {
                    if (!collision.CompareTag("Enemy")) return;
                    collision.GetComponent<HealthSystem>()?.tryHit(attackDamage * 2);
                    Destroy(fireball.gameObject);
                };
                break;
            case 2:
                ProjectileSkill fireball2 = ProjectileSkill.MakeProjectileSkill(fireballPrefab, transform.position + new Vector3(0, -0.75f, 0f), attackPoint.transform.rotation, attackPoint.transform.localScale);
                fireball2.SetSpeed(5f * Mathf.Sign(attackPoint.rotation.y));
                fireball2.transform.localScale *= 2;
                fireball2.onTriggerEnterEvent += (collision) =>
                {
                    if (!collision.CompareTag("Enemy")) return;
                    collision.GetComponent<HealthSystem>()?.tryHit(attackDamage * 5);
                    Destroy(fireball2.gameObject);
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
