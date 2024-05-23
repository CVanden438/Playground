using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image healthbar;
    public Image manabar;
    public TextMeshProUGUI healthText;
    public Health health;

    void Awake()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        instance = this;
    }

    void Start()
    {
        health.onTakeDamage.AddListener(OnTakeDamage);
        health.onHeal.AddListener(OnHeal);
        healthbar.fillAmount = health.currentHealth / health.maxHealth;
    }

    private void OnTakeDamage(float damageAmount)
    {
        healthbar.fillAmount = health.currentHealth / health.maxHealth;
        healthText.text = health.currentHealth + "/" + health.maxHealth;
    }

    private void OnHeal(float healAmount)
    {
        healthbar.fillAmount = health.currentHealth / health.maxHealth;
        healthText.text = health.currentHealth + "/" + health.maxHealth;
    }
}
