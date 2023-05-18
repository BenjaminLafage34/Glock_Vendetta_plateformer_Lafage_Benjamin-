using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemiIA : MonoBehaviour
{
    public float range = 5f; // Port�e de l'ennemi
    public float shootingRange = 2.5f; // Demi-port�e pour tirer la balle
    public Transform player; // R�f�rence au joueur
    public GameObject bulletPrefab; // Pr�fabriqu� de la balle
    public float speed = 2f; // Vitesse de l'ennemi

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= range)
        {
            // Le joueur est dans la port�e de l'ennemi
            if (distanceToPlayer > shootingRange)
            {
                // Le joueur est en dehors de la demi-port�e de tir
                // Fais suivre le joueur � l'ennemi
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                // Le joueur est dans la demi-port�e de tir
                // Tire la balle
                Shoot();
            }
        }
    }

    void Shoot()
    {
        // Instancie une balle � partir du pr�fabriqu� � la position de l'ennemi
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
