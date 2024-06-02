using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject playMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    private float mainVolume = 0;

    private void Start()
    {
        mainMenu.SetActive(true);
        playMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainMenu.activeSelf)
            {
                CloseGame();
            }
            else
            {
                back();
            }
        }
    }

    public void Play()
    {
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
        optionsMenu.SetActive(false);
        //SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    

    public void Credits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
    
    public void back()
    {
        mainMenu.SetActive(true);
        playMenu.SetActive(false);
        creditsMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void newGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial");
    }
    
    //TODO
    public void continueGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void adjustMainVolume(Slider slider)
    {
        mainVolume = slider.value;
        Debug.Log("MainVolume: " + mainVolume);
    }
    
    public void CloseGame()
    { 
        Debug.Log("Quitting...");
        Application.Quit();
    }
    
}
