using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public delegate void CollisionAction(Collider2D collision);
    public event CollisionAction OnCollision;
    private Rigidbody2D rb;
    private Vector2 initialPos;

    [HideInInspector]
    public Vector3 targetPos;

    [HideInInspector]
    public SpellSO data;

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
        float distance = Vector2.Distance(initialPos, transform.position);
        if (data)
        {
            if (distance >= data.range)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
