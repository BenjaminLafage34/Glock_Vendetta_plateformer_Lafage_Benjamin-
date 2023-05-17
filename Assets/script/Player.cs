using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int minRage = 0;
    public int currentRage;
    public Animator animator;
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

        if(currentRage >= 100)
        {
            currentRage = 0;
            Movement2D movement2D = GetComponent<Movement2D>();
        }
    }
    internal void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            
            Destroy(gameObject);

        }
        animator.SetBool("IsDying", true);
    }

}
