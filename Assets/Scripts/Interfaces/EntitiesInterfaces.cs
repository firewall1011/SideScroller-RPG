using System;
using UnityEngine;

/**
 * Interface for taking hits
 */
public interface IDamagable
{
    void tryHit(float damage);
    event Action<float> onDamage;
}

public interface IHealth
{
    event Action<float> onHealthChange;
    event Action onDeath;

    float healthPercent { get; }
}
