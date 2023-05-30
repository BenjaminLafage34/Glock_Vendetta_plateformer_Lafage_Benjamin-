using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int PlayerDead;
    public int minRage = 0;
    public int currentRage;
    public bool Dead = false;
    public Animator animator;
    public RageBar RageBar;
    public HealthBar healthBar;
    public KeyCode specialAttackKey = KeyCode.R;
    public GameOverScreen GameOverScreen;
    SpriteRenderer spriteRenderer;
    public SpriteRenderer gunSpriteRenderer;





    void Start()
    {
        animator = GetComponent<Animator>();
        currentRage = minRage;
        RageBar.SetMinRage(minRage);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(specialAttackKey))
        {
            RageAttack();
           



        }
       
        

        


    }

    public void IncreaseRage(int rage)
    {
        currentRage += rage;
        RageBar.SetRage(currentRage);

        if (currentRage >= 100)
        {
            currentRage = 100;
            RageBar.SetRage(currentRage);
        }
    }

    private void RageAttack()
    {
        if (currentRage >= 100)
        {
            Movement2D movement2D = GetComponent<Movement2D>();
            StartCoroutine(movement2D.RageAttack());
            currentRage = 0;
            RageBar.SetRage(currentRage);
        }
    }

     internal void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        StartCoroutine(TouchedByBullet());

        if (currentHealth <= 0)
        {
            animator.SetBool("IsDying", true);
            Dead = true;

            StartCoroutine(DestroyAfterAnimation());
            PlayerDead = Dead ? 1 : 0;
            GameOver();
        }
        
    }
    
    public void GameOver()
    {
        GameOverScreen.Setup(PlayerDead);
    }
    
    
            
    private IEnumerator DestroyAfterAnimation()
    {
        // Attendre la fin de l'animation de "IsDying"
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Détruire le GameObject
        Destroy(gameObject);
    }
    internal IEnumerator TouchedByBullet()
    {
        SpriteRenderer playerSpriteRenderer = GetComponent<SpriteRenderer>();

        playerSpriteRenderer.color = new Color(1.0f, 0.10f, 0.10f);
        gunSpriteRenderer.color = new Color(1.0f, 0.10f, 0.10f);

        yield return new WaitForSeconds(0.5f);

        playerSpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
        gunSpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
    }


}

