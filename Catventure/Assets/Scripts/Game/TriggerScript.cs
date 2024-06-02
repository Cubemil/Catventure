using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerScript : MonoBehaviour
{
    public GameObject jumpTut;
    public GameObject moveTut;
    public GameObject interactTut;
    public GameObject npcFriendlyTut;
    public GameObject npcEvilTut;
    public GameObject itemTut;
    public TextMeshProUGUI textTut;
    
    
    // Start is called before the first frame update
    void Start()
    {
        textTut.text = "";
    }
    
    
    // Trigger-Ereignis-Methode
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == jumpTut)
        {
            ShowTutorial("Press Space to Jump");
        }
        else if (other.gameObject == moveTut)
        {
            ShowTutorial("Press W/A/S/D to Move");
        }
        else if (other.gameObject == interactTut)
        {
            ShowTutorial("Press E to Interact and to finish the Tutorial");
        }
        else if (other.gameObject == npcFriendlyTut)
        {
            ShowTutorial("go near an NPC and *meow* to talk to them");
            
        }else if (other.gameObject == npcEvilTut)
        {
            ShowTutorial("go near an evil NPC and *meow* to scare them");
        }else if(other.gameObject == itemTut)
        {
            ShowTutorial("walk into the item to get it");
        }
    }


    void OnTriggerExit(Collider other)
        {
            textTut.text = "";
        }

        // Methode zum Anzeigen des Tutorials
        void ShowTutorial(string text)
        {
            textTut.text = text;
        }
    
}