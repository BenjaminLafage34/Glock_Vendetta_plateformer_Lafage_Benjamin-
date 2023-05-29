using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D other)
    {
        // V�rifie si le joueur entre en collision avec la zone de d�g�ts
        if (other.tag == "Player")
        {
            // Obtient la r�f�rence du script du joueur
            Player player = other.GetComponent<Player>();

            // V�rifie si le script du joueur existe
            if (player != null)
            {
                // R�duit la vie du joueur � z�ro
                player.TakeDamage(100);
            }
        }
    }
}