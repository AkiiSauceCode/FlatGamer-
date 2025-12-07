using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using System;

public class PlayerHealth : MonoBehaviour
{
    public event Action OnDamageTaken;
    public event Action OnDeath;

    public int maxHealth;
    public int currentHelth;

    public TMP_Text healthtext;
    public Animator healthanimation;

    public void Start()
    {
        currentHelth = maxHealth;
        healthtext.text = "HP: " + currentHelth + "/" + maxHealth;
    }
    public void ChangeHealth(int amount) 
    {
        currentHelth += amount;

        if(currentHelth > maxHealth) 
        {
            currentHelth = maxHealth;
        }
        else if (currentHelth <= 0) // Death
        {
            OnDeath?.Invoke();
            gameObject.SetActive(false);
            Death();
        }

        else if (amount < 0) // Hurt
        {
            OnDamageTaken?.Invoke();
        }

        healthanimation.Play("TextUpdate"); // Play animation on health change

        healthtext.text = "HP: " + currentHelth + "/" + maxHealth;

        
    }
    private void Death() 
    {
        gameObject.SetActive(false);
    }
}
