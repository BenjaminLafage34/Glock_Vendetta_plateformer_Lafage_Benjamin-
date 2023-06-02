using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematique_fin : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public void Die()
    {
        if ( enemy.Life <= 0)
        {
            Debug.Log("VIE à 0");
            SceneManager.LoadScene("Cinematique_fin");
        }
    }
}
