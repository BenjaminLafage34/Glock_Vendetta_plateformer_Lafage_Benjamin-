using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementObjectVerti : MonoBehaviour
{
    public float vitesseMouvement = 5f; // Vitesse de mouvement de l'objet

    private bool vaEnHaut = true; // Variable booléenne pour déterminer la direction

    private void Update()
    {
        // Déplacer l'objet vers le haut ou vers le bas en fonction de la direction actuelle
        if (vaEnHaut)
        {
            transform.Translate(Vector2.up * vitesseMouvement * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.down * vitesseMouvement * Time.deltaTime);
        }

        // Changer la direction si l'objet atteint un certain point
        if (transform.position.y >= 15f)
        {
            vaEnHaut = false;
        }
        else if (transform.position.y <= 5f)
        {
            vaEnHaut = true;
        }
    }
}
