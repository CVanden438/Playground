using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyProj : MonoBehaviour
{
    public Vector2 direction;

    public EnemySO data;
    Tween tween;

    // Renderer rend;
    public float range;
    public float duration;
    public float size;

    void Awake()
    {
        // rend = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        tween = transform
            .DOMove(direction.normalized * range, duration)
            .OnComplete(Die)
            .SetRelative(true)
            .SetEase(Ease.Linear);
        // rend.material.DOFade(0, 0.7f).SetEase(Ease.OutSine);
        transform.localScale = new Vector3(size, size, 0);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tween.Kill();
            other.GetComponent<Health>().TakeDamage(data.damage);
            Die();
        }
    }
}
