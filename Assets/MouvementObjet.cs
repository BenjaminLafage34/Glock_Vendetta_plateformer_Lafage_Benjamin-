using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementObjet : MonoBehaviour
{
    public float vitesse = 5f; // Vitesse de déplacement du sprite
    public float distance = 5f; // Distance totale à parcourir

    private Vector3 positionInitiale; // Position initiale du sprite
    private float distanceParcourue = 0f; // Distance parcourue jusqu'à présent
    private bool versLeHaut = true; // Indicateur de direction

    private void Start()
    {
        positionInitiale = transform.position; // Enregistre la position initiale du sprite
    }

    private void Update()
    {
        // Calcule le déplacement
        float deplacement = vitesse * Time.deltaTime;

        // Vérifie si le sprite a atteint la distance maximale
        if (distanceParcourue >= distance)
        {
            // Change de direction
            versLeHaut = !versLeHaut;
            distanceParcourue = 0f;
        }

        // Déplace le sprite
        Vector3 deplacementVertical = new Vector3(0f, deplacement, 0f);

        if (versLeHaut)
        {
            transform.Translate(deplacementVertical);
        }
        else
        {
            transform.Translate(-deplacementVertical);
        }

        // Met à jour la distance parcourue
        distanceParcourue += Mathf.Abs(deplacement);
    }
}