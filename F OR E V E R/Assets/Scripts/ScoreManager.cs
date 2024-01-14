using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text timerText;
    public GameObject playerObject;
    private float startTime;
    private bool stopped = false;
    private static float bestTime = Mathf.Infinity;
    private static float lastTime = 0f;
    private float elapsedTime = 0f;

    void Start()
    {
        startTime = Time.time;
        lastTime = PlayerPrefs.GetFloat("LastTime", 0f);
        bestTime = PlayerPrefs.GetFloat("BestTime", Mathf.Infinity);
    }

    void Update()
    {
        if (!stopped)
    {
        if (playerObject != null && playerObject.activeInHierarchy)
        {
            float timeElapsed = Time.time - startTime;
            int minutes = (int)timeElapsed / 60;
            int seconds = (int)timeElapsed % 60;
            int milliseconds = (int)((timeElapsed % 1) * 100);
            timerText.text = string.Format("{0:00}:{1:00}", seconds, milliseconds);
        }
    }
    }

    public void StopTimer()
    {
        stopped = true;
        if (playerObject == null || !playerObject.activeInHierarchy)
        {
            if (elapsedTime < bestTime)
            {
                bestTime = elapsedTime;
                PlayerPrefs.SetFloat("BestTime", bestTime);
            }
            lastTime = elapsedTime;
            PlayerPrefs.SetFloat("LastTime", lastTime);
        }
    }

    public static float GetBestTime()
    {
        return bestTime;
    }

    public static float GetLastTime()
    {
        return lastTime;
    }
}

