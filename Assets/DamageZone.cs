using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le joueur entre en collision avec la zone de dégâts
        if (other.CompareTag("Player"))
        {
            // Obtient la référence du script du joueur
            Player player = other.GetComponent<Player>();

            // Vérifie si le script du joueur existe
            if (player != null)
            {
                // Réduit la vie du joueur à zéro
                player.TakeDamage(0);
            }
        }
    }
}