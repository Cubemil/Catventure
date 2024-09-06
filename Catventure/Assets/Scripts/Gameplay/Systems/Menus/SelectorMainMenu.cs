using UnityEngine;

namespace Gameplay.Systems.Menus
{
    public class SelectorMainMenu : MonoBehaviour
    {
        public GameObject playSelector;  
        public GameObject optionsSelector;  
        public GameObject creditsSelector;  
        public GameObject quitSelector;
        public GameObject newGameSelector;  
        public GameObject continueSelector;  
        public GameObject backSelector;  
        public GameObject oVolumeSelector;  
        public GameObject oBackSelector;
        public GameObject cBackSelector;
    
        public void SelectPlay()
        {
            playSelector.SetActive(true);
            optionsSelector.SetActive(false);
            creditsSelector.SetActive(false);
            quitSelector.SetActive(false);
        }

        public void SelectOptions()
        {
            playSelector.SetActive(false);
            optionsSelector.SetActive(true);
            creditsSelector.SetActive(false);
            quitSelector.SetActive(false);
        }

        public void SelectCredits()
        {
            playSelector.SetActive(false);
            optionsSelector.SetActive(false);
            creditsSelector.SetActive(true);
            quitSelector.SetActive(false);
        }
    
        public void SelectQuit()
        {
            playSelector.SetActive(false);
            optionsSelector.SetActive(false);
            creditsSelector.SetActive(false);
            quitSelector.SetActive(true);
        }

        public void SelectNewGame()
        {
            newGameSelector.SetActive(true);
            continueSelector.SetActive(false);
            backSelector.SetActive(false);
        }

        public void SelectContinue()
        {
            newGameSelector.SetActive(false);
            continueSelector.SetActive(true);
            backSelector.SetActive(false);
        }

        public void SelectBack()
        {
            newGameSelector.SetActive(false);
            continueSelector.SetActive(false);
            backSelector.SetActive(true);
        }

        public void SelectVolume()
        {
            oVolumeSelector.SetActive(true);
            oBackSelector.SetActive(false);
        }

        public void SelectVBack()
        {
            oVolumeSelector.SetActive(false);
            oBackSelector.SetActive(true);
        }

        public void SelectCBack()
        {
            cBackSelector.SetActive(true);
        }
    }
}
