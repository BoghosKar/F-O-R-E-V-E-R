using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHighScore : MonoBehaviour
{
   public Text highScoreText;

    void Start() 
    {
        float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        highScoreText.text = "High Score: " + FormatTime(highScore);
    }

   private string FormatTime(float time)
{
    int minutes = Mathf.FloorToInt(time / 60f);
    int seconds = Mathf.FloorToInt(time % 60f);
    int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
    return string.Format("{1:00}:{2:000}", minutes, seconds, milliseconds);
}

}
