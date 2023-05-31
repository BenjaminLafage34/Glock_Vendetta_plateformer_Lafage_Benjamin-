
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource dialogueSource;

    [Header("---------- Audio Clip----------")]
    public AudioClip background;
    public AudioClip revenge;
    public AudioClip Boss;
    public AudioClip Youwannadie;

    private bool hasTriggered = false; // Variable pour garder une trace de l'état de déclenchement

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            musicSource.clip = Boss;
            musicSource.Play();

            dialogueSource.PlayOneShot(Youwannadie);

            hasTriggered = true; // Définir hasTriggered à true pour indiquer qu'il a été déclenché

            // Désactiver le collider pour empêcher les déclenchements supplémentaires
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }
}