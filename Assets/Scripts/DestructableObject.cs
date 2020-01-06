using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour, IDamagable
{
    public event Action onDamageTaken;

    public void tryHit(float damage)
    {
        onDamageTaken?.Invoke();
        Destroy(gameObject);
    }
}
