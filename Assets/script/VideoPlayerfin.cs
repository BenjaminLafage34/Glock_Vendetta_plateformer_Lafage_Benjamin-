using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine;

public class VideoPlayerfin : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // R�cup�rer le composant VideoPlayer attach� � ce GameObject
        videoPlayer = GetComponent<VideoPlayer>();

        // S'abonner � l'�v�nement de fin de lecture de la vid�o
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
        // Charger la sc�ne "Domaine"
        SceneManager.LoadScene("Menu");
    }
}
