using UnityEngine;

namespace Menu
{
    public class Selector : MonoBehaviour
    {
        public GameObject playSelector;  
        public GameObject optionsSelector;  
        public GameObject creditsSelector;  
    
        public GameObject newGameSelector;  
    
        public GameObject continueSelector;  
    
        public GameObject backSelector;  
    
        public GameObject volumeSelector;  
    
        public GameObject vBackSelector;  
    
        public GameObject resumeSelector;  
    
        public GameObject pOptionsSelector;  
    
        public GameObject quitSelector;  
    
        public void SelectPlay()
        {
            playSelector.SetActive(true);
            optionsSelector.SetActive(false);
            creditsSelector.SetActive(false);
        }
    
        public void SelectOptions()
        {
            playSelector.SetActive(false);
            optionsSelector.SetActive(true);
            creditsSelector.SetActive(false);
        }
    
        public void SelectCredits()
        {
            playSelector.SetActive(false);
            optionsSelector.SetActive(false);
            creditsSelector.SetActive(true);
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
            volumeSelector.SetActive(true);
            vBackSelector.SetActive(false);
        
        }
    
        public void SelectVBack()
        {
            volumeSelector.SetActive(false);
            vBackSelector.SetActive(true);
        }
    
        public void SelectResume()
        {
            resumeSelector.SetActive(true);
            pOptionsSelector.SetActive(false);
            quitSelector.SetActive(false);
        }
    
        public void SelectPOptions()
        {
            resumeSelector.SetActive(false);
            pOptionsSelector.SetActive(true);
            quitSelector.SetActive(false);
        }
    
        public void SelectQuit()
        {
            resumeSelector.SetActive(false);
            pOptionsSelector.SetActive(false);
            quitSelector.SetActive(true);
        }
    }
}
