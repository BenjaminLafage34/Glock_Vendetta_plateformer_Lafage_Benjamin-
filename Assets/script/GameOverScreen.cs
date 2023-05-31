using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    
    public Text pointsText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
         
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Domaine");
    }

    public void RestartButtonEntrepot()
    {
        SceneManager.LoadScene("entrepôt");
    }


    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
