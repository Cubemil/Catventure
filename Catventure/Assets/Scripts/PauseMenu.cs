using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject pauseMenuUi;

    
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        GameIsPaused = true;
        pauseMenuUi.SetActive(GameIsPaused);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        GameIsPaused = false;
        pauseMenuUi.SetActive(GameIsPaused);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    public void MainMenu()
    {
        //Debug.Log("Main Menu loading...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scenes/MainMenuScene");
    }
    

}
