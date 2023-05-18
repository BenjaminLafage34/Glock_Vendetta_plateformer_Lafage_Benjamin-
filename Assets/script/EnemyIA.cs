using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    public Transform Player;
    public float attackRange = 5f;
    public float followDistance = 10f;
    public float attackDelay = 1f;
    private float currentAttackDelay;
    public float moveSpeed = 3f;
    public GameObject projectilePrefab; // Préfabriqué du projectile à instancier
    public Transform projectileSpawnPoint; // Point de spawn du projectile
    public float projectileSpeed = 10f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (currentAttackDelay <= 0f)
            {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
                Vector2 direction = (Player.position - projectileSpawnPoint.position).normalized;
                projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
                currentAttackDelay = attackDelay;

                animator.SetBool("IsShooting", true);
            }
            else
            {
                currentAttackDelay -= Time.deltaTime;
               
            }
        }
        else if (distanceToPlayer <= followDistance)
        {
            animator.SetBool("IsShooting", false);
            Vector2 targetPosition = Player.position - (Player.position - transform.position).normalized * (attackRange / 2f);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (Player.position.x < transform.position.x)
        {
            // Le joueur est à gauche de l'ennemi, donc on le fait tourner vers la gauche
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (Player.position.x > transform.position.x)
        {
            // Le joueur est à droite de l'ennemi, donc on le fait tourner vers la droite
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Permet au joueur de traverser l'ennemi sans le pousser
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
        }
    }
}