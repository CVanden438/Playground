using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamagePopup : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float fadeOutDuration = 1.5f;
    public float destroyDelay = 1.5f;

    private TextMeshPro popupText;
    private Color startColor;

    private void Awake()
    {
        popupText = GetComponent<TextMeshPro>();
        startColor = popupText.color;
    }

    public void Setup(float damageAmount)
    {
        popupText.text = damageAmount.ToString();
        Invoke(nameof(DestroyPopup), destroyDelay);
    }

    private void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        popupText.alpha -= Time.deltaTime / destroyDelay;
        // float elapsedTime = Time.time - (Time.time - destroyDelay + fadeOutDuration);
        // float fadeFactor = Mathf.Clamp01(elapsedTime / fadeOutDuration);
        // popupText.color = new Color(startColor.r, startColor.g, startColor.b, 1f - fadeFactor);
    }

    private void DestroyPopup()
    {
        Destroy(gameObject);
    }
}
