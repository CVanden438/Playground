using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float dashForce;
    public float dashDuration;
    public float dashCooldown;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDir = Vector2.right;
    private bool isDashing;
    private float dashTimer;
    private float dashCooldownTimer;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isDashing)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            movement = new Vector2(moveHorizontal, moveVertical).normalized;

            if (movement != Vector2.zero)
            {
                lastDir = movement;
                UpdateSpriteFlip();
            }
        }

        if (Input.GetMouseButtonDown(1) && !isDashing && dashCooldownTimer <= 0)
        {
            StartDash();
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                StopDash();
            }
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = movement * speed;
        }
    }

    private void UpdateSpriteFlip()
    {
        if (lastDir.x >= 0)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        rb.velocity = lastDir * dashForce;
    }

    private void StopDash()
    {
        isDashing = false;
        dashCooldownTimer = dashCooldown;
        rb.velocity = Vector2.zero;
    }
}

// if (Input.GetKey(KeyCode.W))
// {
//     transform.Translate(Vector2.up * Time.deltaTime * 3);
// }
// if (Input.GetKey(KeyCode.A))
// {
//     transform.Translate(Vector2.left * Time.deltaTime * 3);
// }
// if (Input.GetKey(KeyCode.S))
// {
//     transform.Translate(Vector2.down * Time.deltaTime * 3);
// }
// if (Input.GetKey(KeyCode.D))
// {
//     transform.Translate(Vector2.right * Time.deltaTime * 3);
// }
