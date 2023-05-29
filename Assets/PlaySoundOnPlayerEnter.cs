using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlaySoundOnPlayerEnter : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    private bool hasPlayerEntered = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayerEntered)
        {
            audioSource.Play();
            hasPlayerEntered = true;
        }
    }
}