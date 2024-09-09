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

        private void Update()
        {
            if (_dialogueActive && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
            {
                DisplayNextLine();
            }
        }

        public void StartDialogue(string[] lines)
        {
            dialogueLines = lines;
            _currentLine = 0;
            dialoguePanel.SetActive(true);
            _dialogueActive = true;
            DisplayNextLine();
        }

        private void DisplayNextLine()
        {
            if (_currentLine < dialogueLines.Length)
            {
                dialogueText.text = dialogueLines[_currentLine];
                _currentLine++;
            }
            else
            {
                EndDialogue();
            }
        }

        private void EndDialogue()
        {
            dialoguePanel.SetActive(false);
            _dialogueActive = false;
        }

        public void OnDialoguePanelClick()
        {
            if (_dialogueActive)
            {
                DisplayNextLine();
            }
        }

        public bool IsDialogueActive()
        {
            return _dialogueActive;
        }
    }
}