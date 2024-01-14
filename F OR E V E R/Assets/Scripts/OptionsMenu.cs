using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;


public class OptionsMenu : MonoBehaviour
{

 public PostProcessVolume volume;

 public AudioMixer mixer;


   

    public void ToggleEffects()
    {
        volume.enabled = !volume.enabled;
    }

    public void SetFullScreen(bool isFullScreen)
    {

    Screen.fullScreen = isFullScreen;
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    } 
    

}
