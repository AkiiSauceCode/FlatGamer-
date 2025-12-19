using UnityEngine;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour
{
    public event Action OnDamageTaken;
    public event Action OnDeath;

    public GameObject SharedHealthObject;

    public int maxHealth = 50;
    public int currentHealth = 50;

    public TMP_Text healthtext;

    public Animator anim;
    public Animator healthanimation;

    public GameObject HealthBarUI0;
    public GameObject HealthBarUI1;
    public GameObject HealthBarUI2;
    public GameObject HealthBarUI3;
    public GameObject HealthBarUI4;
    public GameObject HealthBarUI5;

    void Start()
    {
        maxHealth = SharedHealthObject.GetComponent<SharedHealth>().maxHealth;
        currentHealth = SharedHealthObject.GetComponent<SharedHealth>().currentHealth;
        UpdateHealthUI();
    }

        private void OnEnable()
    {
        OnDamageTaken += HandleDamage;
    }
    private void OnDisable()
    {
        OnDamageTaken -= HandleDamage;
    }

    void HandleDamage()
    {
        anim.SetTrigger("isDamaged");
    }

    public void ChangeHealth(int amount)
    {
        SharedHealthObject.GetComponent<SharedHealth>().currentHealth += amount;
        currentHealth = SharedHealthObject.GetComponent<SharedHealth>().currentHealth;


        if (amount < 0 && currentHealth > 0)
        {
            OnDamageTaken?.Invoke();
        }

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
            Death();
            return;
        }
        
        
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        healthtext.text = $"HP: {currentHealth}/{maxHealth}";

        HealthBarUI0.SetActive(false);
        HealthBarUI1.SetActive(false);
        HealthBarUI2.SetActive(false);
        HealthBarUI3.SetActive(false);
        HealthBarUI4.SetActive(false);
        HealthBarUI5.SetActive(false);

        if (currentHealth == 50)
            HealthBarUI5.SetActive(true);
        else if (currentHealth == 40)
            HealthBarUI4.SetActive(true);
        else if (currentHealth == 30)
            HealthBarUI3.SetActive(true);
        else if (currentHealth == 20)
            HealthBarUI2.SetActive(true);
        else if (currentHealth == 10)
            HealthBarUI1.SetActive(true);
        else
            HealthBarUI0.SetActive(true);
    }

    void Death()
    {
        HealthBarUI1.SetActive(false);
        HealthBarUI0.SetActive(true);
        gameObject.SetActive(false);
    }
}
