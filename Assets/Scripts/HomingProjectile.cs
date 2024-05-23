using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public delegate void CollisionAction(Collider2D collision);
    public event CollisionAction OnCollision;
    private Rigidbody2D rb;
    private Vector2 initialPos;

    [HideInInspector]
    public Vector3 targetPos;

    [HideInInspector]
    public SpellSO data;
    Transform target;
    float homingSpeed = 10f;
    public bool homing = false;

    void Awake()
    {
        initialPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Vector3 direction = targetPos - transform.position;
        rb.velocity = (Vector2)direction.normalized * 10;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void Update()
    {
        if (target == null && homing)
        {
            FindClosestEnemy();
        }
        if (target != null)
        {
            // Calculate the direction to the nearest enemy
            Vector3 direction = target.transform.position - transform.position;
            direction.Normalize();

            // Adjust the projectile's velocity towards the enemy
            rb.velocity = Vector2.Lerp(
                rb.velocity,
                direction * homingSpeed,
                Time.deltaTime * homingSpeed
            );

            // Rotate the projectile to face the enemy
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        float distance = Vector2.Distance(initialPos, transform.position);
        if (data)
        {
            if (distance >= data.range)
            {
                Destroy(gameObject);
            }
        }
    }

    void FindClosestEnemy()
    {
        float minDist = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist && dist <= 3)
            {
                minDist = dist;
                target = enemy.transform;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Loot>().GenerateDrops();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
