using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public GameObject playSelector;  
    public GameObject optionsSelector;  
    public GameObject creditsSelector;
    public GameObject closeSelector;
    
    public GameObject newGameSelector;  
    
    public GameObject continueSelector;  
    
    public GameObject backSelector;  
    
    public GameObject volumeSelector;  
    
    public GameObject vBackSelector;  
    
    public GameObject resumeSelector;  
    
    public GameObject pOptionsSelector;  
    
    public GameObject quitSelector;  
    
    public void selectPlay()
    {
        playSelector.SetActive(true);
        optionsSelector.SetActive(false);
        creditsSelector.SetActive(false);
        closeSelector.SetActive(false);
    }
    
    public void selectOptions()
    {
        playSelector.SetActive(false);
        optionsSelector.SetActive(true);
        creditsSelector.SetActive(false);
        closeSelector.SetActive(false);
    }
    
    public void selectCredits()
    {
        playSelector.SetActive(false);
        optionsSelector.SetActive(false);
        creditsSelector.SetActive(true);
        closeSelector.SetActive(false);
    }
    
    public void selectClose()
    {
        playSelector.SetActive(false);
        optionsSelector.SetActive(false);
        creditsSelector.SetActive(false);
        closeSelector.SetActive(true);
    }
    
    public void selectNewGame()
    {
        newGameSelector.SetActive(true);
        continueSelector.SetActive(false);
        backSelector.SetActive(false);
    }
    
    public void selectContinue()
    {
        newGameSelector.SetActive(false);
        continueSelector.SetActive(true);
        backSelector.SetActive(false);
    }
    
    public void selectBack()
    {
        newGameSelector.SetActive(false);
        continueSelector.SetActive(false);
        backSelector.SetActive(true);
    }
    
    public void selectVolume()
    {
        volumeSelector.SetActive(true);
        vBackSelector.SetActive(false);
        
    }
    
    public void selectVBack()
    {
        volumeSelector.SetActive(false);
        vBackSelector.SetActive(true);
    }
    
    public void selectResume()
    {
        resumeSelector.SetActive(true);
        pOptionsSelector.SetActive(false);
        quitSelector.SetActive(false);
    }
    
    public void selectPOptions()
    {
        resumeSelector.SetActive(false);
        pOptionsSelector.SetActive(true);
        quitSelector.SetActive(false);
    }
    
    public void selectQuit()
    {
        resumeSelector.SetActive(false);
        pOptionsSelector.SetActive(false);
        quitSelector.SetActive(true);
    }
}
