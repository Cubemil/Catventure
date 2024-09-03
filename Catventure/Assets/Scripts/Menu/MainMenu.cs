using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject playMenu;
        public GameObject optionsMenu;
        public GameObject creditsMenu;
        public float _mainVolume = 0; //Todo Sound

        private void Start()
        {
            mainMenu.SetActive(true);
            playMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }
        
        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;

            if (mainMenu.activeSelf)
            {
                Quit();
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

        public void Quit()
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
            creditsMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }

        public void NewGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Garden");
        }
    
        //TODO
        public void ContinueGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Flappy Cat");
        }

        public void AdjustMainVolume(Slider slider)
        {
            _mainVolume = slider.value; //Todo Sound
        }
    
    }
}
