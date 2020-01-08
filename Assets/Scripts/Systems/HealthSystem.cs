using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamagable, IHealth
{
    public event Action<float> onHealthChange;
    public event Action onDamageTaken;
    public event Action onDeath;

    [Header("Health Properties")]
    [SerializeField] private float MaxHealth = 0f;
    private float health = 0f;
    public float HealthPercent => health / MaxHealth;

    private void Start()
    {
        health = MaxHealth;
        onDeath += die;
    }

    private void die()
    {
        gameObject.SetActive(false);
    }

    public void tryHit(float damage)
    {
        Debug.Log("Took " + damage + " of damage");

        //Taking damage
        health -= damage;

        //Check death
        if (health <= 0f)
            onDeath?.Invoke();

        //Invoke onDamage and onHealthChange events
        onDamageTaken?.Invoke();
        onHealthChange?.Invoke(HealthPercent);
    }

    private void OnDestroy()
    {
        onDeath -= die;
    }
}
