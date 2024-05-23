using UnityEngine;
using System.Collections;

public class ExplosionAnimation : MonoBehaviour
{
    public Sprite[] explosionSprites; // Array of sprites for the explosion animation
    public float frameTime = 0.3f; // Time duration for each frame

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(PlayExplosionAnimation());
    }

    private IEnumerator PlayExplosionAnimation()
    {
        while (currentFrame < explosionSprites.Length)
        {
            spriteRenderer.sprite = explosionSprites[currentFrame];
            currentFrame++;
            yield return new WaitForSeconds(frameTime);
        }

        // Destroy the explosion object after the animation is complete
        Destroy(gameObject);
    }
}
