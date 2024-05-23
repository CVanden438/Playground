using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveSlash : MonoBehaviour
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
        if (data.weaponType == WeaponType.melee)
        {
            tween = transform
                .DOMove(direction.normalized * data.range, 0.7f)
                .OnComplete(Die)
                .SetRelative(true)
                .SetEase(Ease.OutCubic);
            // rend.material.DOFade(0, 0.7f).SetEase(Ease.OutSine);
        }
        if (data.weaponType == WeaponType.range)
        {
            tween = transform
                .DOMove(direction.normalized * data.range, 0.7f)
                .OnComplete(Die)
                .SetRelative(true)
                .SetEase(Ease.Linear);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Resource"))
        {
            Debug.Log("Hit");
            tween.Kill();
            other.GetComponent<Health>().TakeDamage(data.damage);
            Die();
        }
    }
}
