using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class AscenseurController : MonoBehaviour
{
    public Animator ascenseurAnimator;
    public KeyCode toucheInteraction = KeyCode.E;
    public float distanceMaxInteraction = 2f;
    public Transform player;

    private bool isPlayerNear;

    private void Update()
    {
        if (player == null) return;

        // Vérifie si le joueur est à proximité de l'ascenseur
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        isPlayerNear = distanceToPlayer <= distanceMaxInteraction;

        // Vérifie si le joueur appuie sur la touche d'interaction
        if (isPlayerNear && Input.GetKeyDown(toucheInteraction))
        {
            
            StartCoroutine(WaitForAnimationToEnd());

        }

    }
    IEnumerator WaitForAnimationToEnd()
    {
        // Déclenche l'animation de l'ascenseur
        ascenseurAnimator.SetTrigger("Ouvrir");

        // Attendre la fin de l'animation
        yield return new WaitForSeconds(ascenseurAnimator.GetCurrentAnimatorStateInfo(0).length);

        SceneManager.LoadScene("entrepôt");


    }
}