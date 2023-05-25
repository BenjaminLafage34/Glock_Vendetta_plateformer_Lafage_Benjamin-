using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementObjet : MonoBehaviour
{
    public float vitesse = 5f; // Vitesse de d�placement du sprite
    public float distance = 5f; // Distance totale � parcourir

    private Vector3 positionInitiale; // Position initiale du sprite
    private float distanceParcourue = 0f; // Distance parcourue jusqu'� pr�sent
    private bool versLeHaut = true; // Indicateur de direction

    private void Start()
    {
        positionInitiale = transform.position; // Enregistre la position initiale du sprite
    }

    private void Update()
    {
        // Calcule le d�placement
        float deplacement = vitesse * Time.deltaTime;

        // V�rifie si le sprite a atteint la distance maximale
        if (distanceParcourue >= distance)
        {
            // Change de direction
            versLeHaut = !versLeHaut;
            distanceParcourue = 0f;
        }

        // D�place le sprite
        Vector3 deplacementVertical = new Vector3(0f, deplacement, 0f);

        if (versLeHaut)
        {
            transform.Translate(deplacementVertical);
        }
        else
        {
            transform.Translate(-deplacementVertical);
        }

        // Met � jour la distance parcourue
        distanceParcourue += Mathf.Abs(deplacement);
    }
}