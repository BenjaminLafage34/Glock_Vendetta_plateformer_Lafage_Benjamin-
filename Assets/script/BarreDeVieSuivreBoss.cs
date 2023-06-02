using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreDeVieSuivreBoss : MonoBehaviour
{
    public Transform bossTransform; // Référence au transform du boss
    public float offset = 0.5f;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (bossTransform != null)
        {
            // Utilisez la position du boss pour positionner la barre de vie
            Vector3 bossPosition = bossTransform.position;
            Vector3 barPosition = Camera.main.WorldToScreenPoint(bossPosition);
            rectTransform.position = new Vector3(barPosition.x, barPosition.y + offset, barPosition.z);
        }
    }
}