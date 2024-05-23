using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Health healthScript;
    public Image fillImage;
    public Image damageImage;
    public float fillSpeed = 3f;

    // private float targetFillAmount;
    // private float lastDamageAmount;

    void Awake()
    {
        healthScript = GetComponentInParent<Health>();
    }

    private void Start()
    {
        // if (healthScript == null)
        // {
        //     healthScript = FindObjectOfType<Health>();
        // }

        healthScript.onTakeDamage.AddListener(OnTakeDamage);
        healthScript.onHeal.AddListener(OnHeal);

        fillImage.fillAmount = healthScript.currentHealth / healthScript.maxHealth;
        damageImage.fillAmount = fillImage.fillAmount;
        // targetFillAmount = fillImage.fillAmount;
    }

    private void OnTakeDamage(float damageAmount)
    {
        // lastDamageAmount = damageAmount;
        // targetFillAmount = healthScript.currentHealth / healthScript.maxHealth;
        fillImage.fillAmount = healthScript.currentHealth / healthScript.maxHealth;
    }

    private void OnHeal(float healAmount)
    {
        // targetFillAmount = healthScript.currentHealth / healthScript.maxHealth;
        fillImage.fillAmount = healthScript.currentHealth / healthScript.maxHealth;
    }

    private void Update()
    {
        // if (fillImage.fillAmount > targetFillAmount)
        // {
        //     fillImage.fillAmount -= fillSpeed * Time.deltaTime;
        //     fillImage.fillAmount = Mathf.Max(fillImage.fillAmount, targetFillAmount);
        // }

        if (damageImage.fillAmount > fillImage.fillAmount)
        {
            // float damageFillAmount = targetFillAmount + lastDamageAmount / healthScript.maxHealth;
            damageImage.fillAmount -= fillSpeed * Time.deltaTime;
            damageImage.fillAmount = Mathf.Clamp(damageImage.fillAmount, fillImage.fillAmount, 1);
        }
    }

    private void OnDisable()
    {
        if (healthScript != null)
        {
            healthScript.onTakeDamage.RemoveListener(OnTakeDamage);
            healthScript.onHeal.RemoveListener(OnHeal);
        }
    }
}
