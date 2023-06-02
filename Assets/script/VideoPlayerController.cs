using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
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
            
            SceneManager.LoadScene("Domaine");
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Charger la scène "Domaine"
        SceneManager.LoadScene("Domaine");
    }
}
