using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpriteDisplay : MonoBehaviour
{
    [SerializeField] private GameObject youWannaDie; // Référence à l'objet cible avec le SpriteRenderer

    private SpriteRenderer spriteRenderer; // Référence au SpriteRenderer de l'objet cible

    private bool hasTriggered = false; // Variable pour garder une trace de l'état de déclenchement

    public void Start()
    {
        spriteRenderer = youWannaDie.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // Désactiver le SpriteRenderer au démarrage
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && spriteRenderer != null && !hasTriggered)
        {
            spriteRenderer.enabled = true; // Activer le SpriteRenderer pour afficher le sprite

            Invoke("HideSprite", 5f); // Appeler la fonction HideSprite après 4 secondes

            hasTriggered = true; // Définir hasTriggered à true pour indiquer qu'il a été déclenché
        }
    }

    public void HideSprite()
    {
        spriteRenderer.enabled = false; // Désactiver le SpriteRenderer pour masquer le sprite
    }
}