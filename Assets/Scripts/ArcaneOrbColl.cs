using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneOrbColl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
        }
    }
}
