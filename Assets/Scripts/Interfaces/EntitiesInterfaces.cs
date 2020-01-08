using System;
using UnityEngine;

/**
 * Interface for taking hits
 */
public interface IDamagable
{
    void tryHit(float damage);
    event Action onDamageTaken;
}

public interface IHealth
{
    event Action<float> onHealthChange;
    event Action onDeath;

    float HealthPercent { get; }
}

public interface IAttack
{
    event Action onAttackEvent;
}

public interface IMovable
{
    event Action onCrouchEvent;
    event Action<bool> onJumpEvent;
    event Action<float> onMoveEvent;
}
