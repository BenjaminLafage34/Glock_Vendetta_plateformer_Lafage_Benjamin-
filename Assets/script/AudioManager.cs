
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source----------")]
    [SerializeField] AudioSource musicSource;

    [Header("---------- Audio Clip----------")]
    public AudioClip background;
    public AudioClip revenge;
    public AudioClip Boss;
   
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    //private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            musicSource.PlayOneShot(revenge);
            Debug.Log("PLayer Enter into Boss");
            // Changer la musique ici
            musicSource.clip = Boss;
            musicSource.Play();
        }
    }

}

