using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpriteDisplay : MonoBehaviour
{
    [SerializeField] private GameObject youWannaDie; // R�f�rence � l'objet cible avec le SpriteRenderer

    private SpriteRenderer spriteRenderer; // R�f�rence au SpriteRenderer de l'objet cible

    private bool hasTriggered = false; // Variable pour garder une trace de l'�tat de d�clenchement

    public void Start()
    {
        spriteRenderer = youWannaDie.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // D�sactiver le SpriteRenderer au d�marrage
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && spriteRenderer != null && !hasTriggered)
        {
            spriteRenderer.enabled = true; // Activer le SpriteRenderer pour afficher le sprite

            Invoke("HideSprite", 5f); // Appeler la fonction HideSprite apr�s 4 secondes

            hasTriggered = true; // D�finir hasTriggered � true pour indiquer qu'il a �t� d�clench�
        }
    }

    public void HideSprite()
    {
        spriteRenderer.enabled = false; // D�sactiver le SpriteRenderer pour masquer le sprite
    }
}