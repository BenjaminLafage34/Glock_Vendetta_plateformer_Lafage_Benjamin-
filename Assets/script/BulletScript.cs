using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public string bulletLayer = "Bullets"; // Nom du Layer pour les balles
    public string enemyLayer = "Enemi"; // Nom du Layer pour les ennemis

    void Start()
    {
        // Affecte le Layer "Bullets" à l'objet balle
        gameObject.layer = LayerMask.NameToLayer(bulletLayer);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Player p = col.gameObject.GetComponent<Player>();
            p.TakeDamage(3);
        }
        else if (col.gameObject.CompareTag("Sol"))
        {
            Destroy(gameObject);
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer(enemyLayer))
        {
            // Ne rien faire si la collision est avec un ennemi (layer "Ennemi")
        }
    }
}
