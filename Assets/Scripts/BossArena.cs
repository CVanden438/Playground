using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossArena : MonoBehaviour
{
    public GameObject arenaBounds;
    public GameObject boss;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            arenaBounds.SetActive(true);
            boss.GetComponent<CircleCollider2D>().enabled = true;
            boss.GetComponent<Enemy>().enabled = true;
            boss.GetComponent<EnemyMovement>().enabled = true;
        }
    }
}
