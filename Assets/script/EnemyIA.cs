using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyIA : MonoBehaviour
{/*
    public Transform Player;
    public float attackRange = 20f;
    public float followDistance = 20f;
    public float attackDelay = 1f;
    private float currentAttackDelay;
    public float moveSpeed = 4f;
    public GameObject projectilePrefab; // Préfabriqué du projectile à instancier
    public Transform projectileSpawnPoint; // Point de spawn du projectile
    public float projectileSpeed = 5f;

    public LayerMask groundLayer; // Couche de collision pour la surface du sol

    private bool isFacingRight = true; // Variable pour le sens de l'ennemi

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
            }
            else
            {
                currentAttackDelay -= Time.deltaTime;
            }
        }
        else
        {
            Vector2 targetPosition = Player.position - (Player.position - transform.position).normalized * (attackRange / 10f);
            RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer);
            if (groundHit.collider != null)
            {
                targetPosition.y = groundHit.point.y; // Ajuster la position en fonction de la hauteur du sol
            }
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Logique de retournement
            if (Player.position.x > transform.position.x && !isFacingRight)
            {
                Flip();
            }
            else if (Player.position.x < transform.position.x && isFacingRight)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }*/
}