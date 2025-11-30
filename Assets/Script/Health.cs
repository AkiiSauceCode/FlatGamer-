using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHelth;

    public TMP_Text healthtext;
    public Animator healthanimation;

    public void Start()
    {
        healthtext.text = "HP: " + currentHelth + "/" + maxHealth;
    }
    public void ChangeHealth(int amount) 
    {
        currentHelth += amount;
        healthanimation.Play("TextUpdate");

        healthtext.text = "HP: " + currentHelth + "/" + maxHealth;

        if (currentHelth <= 0) 
        {
            gameObject.SetActive(false);
        }
    }
}
