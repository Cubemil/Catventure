using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Gameplay.Systems.Menus
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject playMenu;
        public GameObject optionsMenu;
        public GameObject creditsMenu;
        public float mainVolume;
        public string lastSavedScene;
        
        public Animator transition;
        public float transitionTime = 1f;
        
        private void Start()
        {
            mainMenu.SetActive(true);
            playMenu.SetActive(false);
            optionsMenu.SetActive(false);
            
            //TODO implement save system:
            lastSavedScene = "ApartmentCutscene";
        }

        public void Play()
        {
            mainMenu.SetActive(false);
            playMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }

        public void Options()
        {
            optionsMenu.SetActive(true);
            mainMenu.SetActive(false);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    
        public void Back()
        {
            mainMenu.SetActive(true);
            playMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }

        public void NewGame()
        {
            StartCoroutine(DelayedSceneChange(transitionTime));
            Time.timeScale = 1f;
            SceneManager.LoadScene($"ApartmentCutscene");
        }
    
        public void ContinueGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(lastSavedScene);
        }

        public void AdjustMainVolume(Slider volumeSlider)
        {
            mainVolume = volumeSlider.value;
        }
    
        private IEnumerator DelayedSceneChange(float delay)
        {
            // Trigger the transition animation
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(delay); // Wait for the specified delay
        }
    }
}
