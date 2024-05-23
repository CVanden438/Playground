using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveArrow : MonoBehaviour
{
    public Vector2 direction;
    public WeaponSO data;
    Tween tween;
    Renderer rend;

    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        tween = transform
            .DOMove(direction.normalized * data.range, 0.7f)
            .OnComplete(Die)
            .SetRelative(true)
            .SetEase(Ease.Linear);
        // rend.material.DOFade(0, 0.7f).SetEase(Ease.OutSine);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            tween.Kill();
            Die();
        }
    }
}
