using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Systems.Managers
{
    [System.Serializable]
    public class Dialogue
    {
        public string npcName;
        public List<string> sentences;
    }
    
    public class DialogueManager : MonoBehaviour
    {
        public Text dialogueText;
        public GameObject dialoguePanel;
        private Queue<string> _sentences;

        private void Start()
        {
            _sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            dialoguePanel.SetActive(true);
            _sentences.Clear();

            foreach (var sentence in dialogue.sentences)
            {
                _sentences.Enqueue(sentence);                
            }

            DisplayNextSentence();
        }

        private void DisplayNextSentence()
        {
            if (_sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            var sentence = _sentences.Dequeue();
            dialogueText.text = sentence;
        }

        private void EndDialogue()
        {
            dialoguePanel.SetActive(false);
        }
    }
}