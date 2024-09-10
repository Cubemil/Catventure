using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay.Systems.Menus
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject playMenu;
        public GameObject optionsMenu;
        public GameObject creditsMenu;
        public float mainVolume = 0; //TODO Sound

        private void Start()
        {
            mainMenu.SetActive(true);
            playMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;

            if (mainMenu.activeSelf)
            {
                QuitGame();
            }
            else
            {
                Back();
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

        public void QuitGame()
        {
            Debug.Log("Quitting...");
            Application.Quit();
        }

        public void Credits()
        {
            mainMenu.SetActive(false);
            creditsMenu.SetActive(true);
        }
    
        public void Back()
        {
            mainMenu.SetActive(true);
            playMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }

        public void NewGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Apartment");
        }
    
        //TODO
        public void ContinueGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Flappy Cat");
        }

        public void AdjustMainVolume(Slider slider)
        {
            mainVolume = slider.value; //Todo Sound
        }
    
    }
}
