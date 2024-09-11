using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Gameplay.Systems.Managers
{
    public class DialogueManager : MonoBehaviour
    {
        public GameObject dialoguePanel;
        public TextMeshProUGUI dialogueText;
        public string[] dialogueLines;
        private int _currentLine;
        private bool _dialogueActive;
        
        // UI-raycasting for clicking through dialogue box
        private GraphicRaycaster _graphicRaycaster;
        private EventSystem _eventSystem;

        private void Start()
        {
            _graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
            _eventSystem = EventSystem.current;
        }

        private void Update()
        {
            if (!_dialogueActive) return;
            if (Input.GetKeyDown(KeyCode.E)) DisplayNextLine();

            if (!Input.GetMouseButtonDown(0)) return;
            if (IsPointerOverUIElement()) DisplayNextLine();
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
            else EndDialogue();
        }

        private void EndDialogue()
        {
            dialoguePanel.SetActive(false);
            _dialogueActive = false;
        }

        public bool IsDialogueActive()
        {
            return _dialogueActive;
        }
        
        private bool IsPointerOverUIElement()
        {
            var pointerEventData = new PointerEventData(_eventSystem)
            {
                position = Input.mousePosition
            };
            
            // raycast for UI-elements
            var results = new System.Collections.Generic.List<RaycastResult>();
            _graphicRaycaster.Raycast(pointerEventData, results);

            return results.Any(result => result.gameObject == dialoguePanel);
        }
    }
}