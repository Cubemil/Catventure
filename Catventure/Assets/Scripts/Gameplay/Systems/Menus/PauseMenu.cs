using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Gameplay.Systems.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        private static bool _gameIsPaused;
        public GameObject pauseMenu;
        public GameObject optionsMenu;
        public float mainVolume;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            
            if (_gameIsPaused)
                Resume();
            else
                Pause();
        }

        private void Pause()
        {
            _gameIsPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            _gameIsPaused = false;
            pauseMenu.SetActive(_gameIsPaused);
            optionsMenu.SetActive(_gameIsPaused);
            Time.timeScale = 1f;
        }

        public void MainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
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
    
        public void AdjustMainVolume(Slider slider)
        {
            mainVolume = slider.value;
        }
    }
}
