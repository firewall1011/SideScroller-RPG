using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour, IDamagable
{
    public event Action<float> onDamage;

    public void tryHit(float damage)
    {
        onDamage?.Invoke(damage);
        Destroy(gameObject);
    }
}
