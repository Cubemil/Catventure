using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {
        private static bool _gameIsPaused;
        public GameObject pauseMenu;
        public GameObject optionsMenu;
        public PostProcessVolume postProcessVolume;
    
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        private void Pause()
        {
            _gameIsPaused = true;
            pauseMenu.SetActive(true);
            postProcessVolume.enabled = true;
            Time.timeScale = 0f;
        }

        private void Resume()
        {
            _gameIsPaused = false;
            pauseMenu.SetActive(_gameIsPaused);
            optionsMenu.SetActive(_gameIsPaused);
            postProcessVolume.enabled = false;
            Time.timeScale = 1f;
        }

        public void Quit()
        {
            //Debug.Log("Quitting...");
            SceneManager.LoadScene("Menu");
        }

        public void Options()
        {
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
    
        public void LeaveOptions()
        {
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    
        public void MainMenu()
        {
            //Debug.Log("Main Menu loading...");
            Time.timeScale = 1f;
            SceneManager.LoadScene("Game");
        }
    }
}
