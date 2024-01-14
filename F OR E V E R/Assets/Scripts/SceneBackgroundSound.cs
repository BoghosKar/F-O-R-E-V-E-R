using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBackgroundSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if the AudioSource exists and is not playing
        if (audioSource != null && !audioSource.isPlaying)
        {
            // Play the background sound
            audioSource.Play();
        }
    }

    void Update()
    {
        // Check if Time.timeScale is 0 (paused) and pause the music
        if (audioSource != null)
        {
            if (Time.timeScale == 0f && audioSource.isPlaying)
            {
                PauseMusic();
            }
            else if (Time.timeScale == 1f && !audioSource.isPlaying)
            {
                ResumeMusic();
            }
        }
    }



    private void PauseMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }


    private void ResumeMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }
}
