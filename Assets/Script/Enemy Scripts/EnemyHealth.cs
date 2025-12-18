using System;
using UnityEngine;
using UnityEngine.XR;

public class EnemyHealth : MonoBehaviour
{
    public Animator anim;
    public event Action OnDamageTaken;
    public event Action OnDeath;


    public int maxHealth = 10;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
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
    

    public void ChangeHealth(int damage)
    {
        currentHealth += damage;

        if (currentHealth <= 0)
        {   
            OnDeath?.Invoke();
            Die();
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (damage < 0) // Took damage
        {
            OnDamageTaken?.Invoke();
        }
        
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
