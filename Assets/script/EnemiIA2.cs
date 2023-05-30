using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemiIA : MonoBehaviour
{
    public float range = 5f; // Portée de l'ennemi
    public float shootingRange = 2.5f; // Demi-portée pour tirer la balle
    public Transform player; // Référence au joueur
    public GameObject bulletPrefab; // Préfabriqué de la balle
    public float speed = 2f; // Vitesse de l'ennemi

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= range)
        {
            // Le joueur est dans la portée de l'ennemi
            if (distanceToPlayer > shootingRange)
            {
                // Le joueur est en dehors de la demi-portée de tir
                // Fais suivre le joueur à l'ennemi
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                // Le joueur est dans la demi-portée de tir
                // Tire la balle
                Shoot();
            }
        }
    }

    void Shoot()
    {
        // Instancie une balle à partir du préfabriqué à la position de l'ennemi
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
