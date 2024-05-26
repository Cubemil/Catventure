using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject PlayMenuPanel;
    public GameObject OptionsMenuPanel;
    public float MainVolume = 0;

    private void Start()
    {
        MainMenuPanel.SetActive(true);
        PlayMenuPanel.SetActive(false);
        OptionsMenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MainMenuPanel.activeSelf)
            {
                Quit();
            }
            else
            {
                back();
            }
        }
    }

    public void Play()
    {
        MainMenuPanel.SetActive(false);
        PlayMenuPanel.SetActive(true);
        OptionsMenuPanel.SetActive(false);
        SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        MainMenuPanel.SetActive(false);
        PlayMenuPanel.SetActive(false);
        OptionsMenuPanel.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
    
    public void back()
    {
        MainMenuPanel.SetActive(true);
        PlayMenuPanel.SetActive(false);
        OptionsMenuPanel.SetActive(false);
    }

    public void newGame()
    {
        SceneManager.LoadScene("Scenes/PauseMenuScene");
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
    
    //TODO
    public void continueGame()
    {
        newGame();
    }

    public void adjustMainVolume(Slider slider)
    {
        MainVolume = slider.value;
        Debug.Log("MainVolume: " + MainVolume);
    }
    
}
