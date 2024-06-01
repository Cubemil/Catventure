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
    public GameObject npcTut;
    public GameObject itemTut;
    public TextMeshProUGUI textTut;
    
    
    // Start is called before the first frame update
    void Start()
    {
        textTut.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
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
        else if (other.gameObject == npcTut)
        {
            ShowTutorial("go near an NPC and *meow* to talk to them");
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