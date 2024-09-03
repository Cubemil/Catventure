using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorPauseMenu : MonoBehaviour
{
    public GameObject resumeSelector;   
    public GameObject optionsSelector; 
    public GameObject quitSelector; 
    public GameObject volumeSelector;  
    public GameObject vBackSelector;  
    

    public void SelectResume()
    {
        resumeSelector.SetActive(true);
        optionsSelector.SetActive(false);
        quitSelector.SetActive(false);
    }

    public void SelectPOptions()
    {
        resumeSelector.SetActive(false);
        optionsSelector.SetActive(true);
        quitSelector.SetActive(false);
    }

    public void SelectQuit()
    {
        resumeSelector.SetActive(false);
        optionsSelector.SetActive(false);
        quitSelector.SetActive(true);
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
}
