using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLoop : MonoBehaviour
{
    public Sprite[] sprites;
    public float frameTime = 0.3f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        while (true)
        {
            spriteRenderer.sprite = sprites[currentFrame];
            if (currentFrame == sprites.Length - 1)
            {
                currentFrame = 0;
            }
            else
            {
                currentFrame++;
            }
            yield return new WaitForSeconds(frameTime);
        }
    }
}
