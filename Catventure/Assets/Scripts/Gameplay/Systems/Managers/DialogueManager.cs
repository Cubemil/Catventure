using TMPro;
using UnityEngine;

namespace Gameplay.Systems.Managers
{
    public class DialogueManager : MonoBehaviour
    {
        public GameObject dialoguePanel;
        public TextMeshProUGUI dialogueText;
        public string[] dialogueLines;
        private int _currentLine;
        private bool _dialogueActive;
        private bool _isFinalLine;
        
        private void Update()
        {
            if (!_dialogueActive) return;

            if (!Input.GetKeyDown(KeyCode.E)) return;
            if (_isFinalLine)
                EndDialogue();   
            else
                DisplayNextLine();
        }

        public void StartDialogue(string[] lines)
        {
            dialogueLines = lines;
            _currentLine = 0;
            _isFinalLine = false;
            
            dialoguePanel.SetActive(true);
            _dialogueActive = true;
            
            DisplayNextLine();
        }

        private void DisplayNextLine()
        {
            if (_currentLine < dialogueLines.Length - 1)
            {
                dialogueText.text = dialogueLines[_currentLine];
                _currentLine++;
            }
            else
            {
                dialogueText.text = dialogueLines[_currentLine];
                _isFinalLine = true;
            }
        }

        private void EndDialogue()
        {
            dialoguePanel.SetActive(false);
            _dialogueActive = false;
            _currentLine = 0;
            _isFinalLine = false;
        }

        public bool IsDialogueActive()
        {
            return _dialogueActive;
        }
    }
}