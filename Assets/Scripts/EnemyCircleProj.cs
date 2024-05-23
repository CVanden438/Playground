using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyCircleProj : MonoBehaviour
{
    public GameObject projPivot;
    public Vector3 direction;
    Tween tween;
    public int count;

    void Start()
    {
        Setup();
        tween = transform
            .DOMove(direction.normalized * 5, 2f)
            .OnComplete(Die)
            .SetRelative(true)
            .SetEase(Ease.OutCubic);
        // rend.material.DOFade(0, 0.7f).SetEase(Ease.OutSine);
    }

    public void Setup()
    {
        var angle = 360 / count;
        for (int i = 0; i < count; i++)
        {
            Instantiate(projPivot, transform.position, Quaternion.Euler(0, 0, angle * i));
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
