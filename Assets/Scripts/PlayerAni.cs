using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    public Sprite[] idleSprites;
    public Sprite[] moveSprites;
    public bool isMoving = false;
    public float frameTime = 0.3f; // Time duration for each frame

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // EnemySO data = GetComponent<Enemy>().data;
        // idleSprites = data.idleSprites;
        // moveSprites = data.moveSprites;
    }

    private void Start()
    {
        StartCoroutine(Animation());
    }

    public void SetMoving()
    {
        if (!isMoving)
        {
            currentFrame = 0;
        }
        isMoving = true;
    }

    public void SetIdle()
    {
        if (isMoving)
        {
            currentFrame = 0;
        }
        isMoving = false;
    }

    private IEnumerator Animation()
    {
        while (true)
        {
            if (!isMoving)
            {
                spriteRenderer.sprite = idleSprites[currentFrame];
            }
            else
            {
                spriteRenderer.sprite = moveSprites[currentFrame];
            }
            currentFrame++;
            yield return new WaitForSeconds(frameTime);
            if (!isMoving && currentFrame == idleSprites.Length)
            {
                currentFrame = 0;
            }
            if (isMoving && currentFrame == moveSprites.Length)
            {
                currentFrame = 0;
            }
        }
    }
}
