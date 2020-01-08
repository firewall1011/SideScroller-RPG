using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkill : MonoBehaviour
{
    public event Action<Collision2D> onCollisionEnterEvent;
    public event Action<Collider2D> onTriggerEnterEvent;
    public event Action onLifeEnded;

    [SerializeField] private Sprite[] spriteSequence = null;

    private SpriteRenderer spriteRenderer;
    private Utils.Timer[] timers;
    private int currentSprite = 0;

    public static ProjectileSkill MakeProjectileSkill(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        GameObject ins = Instantiate(prefab, position, rotation);
        ins.transform.localScale = localScale;

        return ins.GetComponent<ProjectileSkill>();
    }
    
    public void SetSpeed(float value)
    {
        GetComponent<HorizontalMove>().speed = value;
    }

    private void Start()
    {
        onLifeEnded += LifeEnded;

        spriteRenderer = GetComponent<SpriteRenderer>();
        // == Setting up timer == //
        timers = GetComponents<Utils.Timer>();
        timers[0].SetCooldown(0.3f);
        timers[0].onCooldownEnded += NextSprite;
        timers[0].StartTimer();

        timers[1].SetCooldown(3f);
        timers[1].onCooldownEnded += () => onLifeEnded?.Invoke();
        timers[1].StartTimer();

    }

    private void NextSprite()
    {
        currentSprite = (currentSprite + 1) % spriteSequence.Length;
        spriteRenderer.sprite = spriteSequence[currentSprite];
        timers[0].ResetTimer();
        timers[0].StartTimer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnterEvent?.Invoke(collision);
    }

    private void LifeEnded()
    {
        Destroy(gameObject);
    }
}
