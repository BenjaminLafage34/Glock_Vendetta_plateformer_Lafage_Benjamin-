
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
    public AudioClip YouGotBalls;
    public AudioClip YouCameto;
    public AudioClip ComeOn;
    public AudioClip Guys; 

    private bool hasTriggered = false; // Variable pour garder une trace de l'�tat de d�clenchement

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            //musicSource.clip = Boss;
            //musicSource.Play();
            dialogueSource.PlayOneShot(YouGotBalls, 3f);
            dialogueSource.PlayOneShot(Youwannadie);
            dialogueSource.PlayOneShot(YouCameto, 5f);
            dialogueSource.PlayOneShot(ComeOn, 3f);
            dialogueSource.PlayOneShot(Guys, 2f);


            hasTriggered = true; // D�finir hasTriggered � true pour indiquer qu'il a �t� d�clench�

            // D�sactiver le collider pour emp�cher les d�clenchements suppl�mentaires
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            

        }
    }

}