using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Systems.Managers
{
    public class DialogueManager : MonoBehaviour
    {
        public GameObject dialoguePanel;
        public TextMeshProUGUI dialogueText;
        public GameObject backgroundImage;
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

        // ReSharper disable Unity.PerformanceAnalysis
        public void StartDialogue(string[] lines, Sprite image)
        {
            dialogueLines = lines;
            _currentLine = 0;
            _isFinalLine = false;
            
            dialoguePanel.SetActive(true);
            _dialogueActive = true;

            backgroundImage.GetComponent<Image>().sprite = image;
            
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