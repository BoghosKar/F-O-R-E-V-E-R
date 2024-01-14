using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject controlsMenu;

   void Start() 
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);    


        
    }
    public void LoadLevelByNumber(int LevelID)
    {
        SceneManager.LoadScene(LevelID);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ControlsBackToMenu()
    {
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OptionsButton()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ControlsButton()
    {
        controlsMenu.SetActive(true);
        mainMenu.SetActive(false);   
    }
}
