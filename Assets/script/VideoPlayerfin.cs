using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine;

public class VideoPlayerfin : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Récupérer le composant VideoPlayer attaché à ce GameObject
        videoPlayer = GetComponent<VideoPlayer>();

        // S'abonner à l'événement de fin de lecture de la vidéo
        videoPlayer.loopPointReached += OnVideoFinished;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            SceneManager.LoadScene("Menu");
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Charger la scène "Domaine"
        SceneManager.LoadScene("Menu");
    }
}
