using UnityEngine;

public class SharedHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth = 50;

    void Start()
    {
        currentHealth = maxHealth;
    }
}
