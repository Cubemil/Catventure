using System;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Systems.Managers
{
    public class DialogueManager : MonoBehaviour
    {
        public TextMeshProUGUI dialoguePanel;
        public string[] dialogueLines;
        private int _currentLine = 0;
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
            dialoguePanel.gameObject.SetActive(true);
            _dialogueActive = true;
            DisplayNextLine();
        }

        private void DisplayNextLine()
        {
            if (_currentLine < dialogueLines.Length)
            {
                dialoguePanel.text = dialogueLines[_currentLine];
                _currentLine++;
            }
            else
            {
                EndDialogue();
            }
        }

        private void EndDialogue()
        {
            dialoguePanel.gameObject.SetActive(false);
            _dialogueActive = false;
        }

        public void OnDialoguePanelClick()
        {
            if (_dialogueActive)
            {
                DisplayNextLine();
            }
        }
    }
}