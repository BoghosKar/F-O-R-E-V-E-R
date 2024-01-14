using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static bool created = false;
    private AudioSource audioSource;

    void Awake()
    {
        if (!created)
        {
            // If this is the first instance of the background music, mark it as created
            created = true;
            DontDestroyOnLoad(transform.gameObject);

            // Get the AudioSource component and play it
            audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
        else
        {
            // If the background music has already been created, destroy the new instance
            Destroy(gameObject);
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

    // Add this function to pause the music
    private void PauseMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    // Add this function to resume the music
    private void ResumeMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }
}
