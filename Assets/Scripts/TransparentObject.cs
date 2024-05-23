using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    float transparencyAmount = 0.5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpriteRenderer sr = GetComponentInParent<SpriteRenderer>();
            Color objColor = sr.color;
            objColor.a = transparencyAmount;
            sr.color = objColor;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpriteRenderer sr = GetComponentInParent<SpriteRenderer>();
            Color objColor = sr.color;
            objColor.a = 1;
            sr.color = objColor;
        }
    }
}
