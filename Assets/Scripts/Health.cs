using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isInvincible = false;
    public float invincibilityDuration = 0f;
    public GameObject damagePopupPrefab;
    public UnityEvent<float> onTakeDamage;
    public UnityEvent<float> onHeal;
    public UnityEvent onDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        if (isInvincible)
            return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        onTakeDamage.Invoke(damageAmount);
        if (damagePopupPrefab != null)
        {
            ShowDamagePopup(damageAmount);
        }
        if (currentHealth <= 0f)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        onHeal.Invoke(healAmount);
    }

    public void SetInvincible(bool invincible)
    {
        isInvincible = invincible;
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void ShowDamagePopup(float damageAmount)
    {
        GameObject popupInstance = Instantiate(
            damagePopupPrefab,
            transform.position,
            Quaternion.identity
        );
        DamagePopup popupScript = popupInstance.GetComponent<DamagePopup>();
        popupScript.Setup(damageAmount);
    }

    private void Die()
    {
        onDeath.Invoke();
        Destroy(gameObject);
        // Add any additional death logic here (e.g., game over, respawn, etc.)
    }
}
