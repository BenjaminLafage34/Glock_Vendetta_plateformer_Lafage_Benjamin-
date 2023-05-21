using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AscenseurController : MonoBehaviour
{
    public Animator ascenseurAnimator;
    public KeyCode toucheInteraction = KeyCode.E;
    public float distanceMaxInteraction = 2f;
    public Transform player;

    private bool isPlayerNear;

    private void Update()
    {
        // Vérifie si le joueur est à proximité de l'ascenseur
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        isPlayerNear = distanceToPlayer <= distanceMaxInteraction;

        // Vérifie si le joueur appuie sur la touche d'interaction
        if (isPlayerNear && Input.GetKeyDown(toucheInteraction))
        {
            // Déclenche l'animation de l'ascenseur
            ascenseurAnimator.SetTrigger("Ouvrir");
        }
    }
}