using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int minRage = 0;
    public int currentRage;

    public RageBar RageBar;

    public HealthBar healthBar;
    void Start()
    {
        currentRage = minRage;
        RageBar.SetMinRage(minRage);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseRage(3);
        }

    }
    public void IncreaseRage(int rage)
    {
        currentRage += rage;
        RageBar.SetRage(currentRage);
    }
    internal void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
