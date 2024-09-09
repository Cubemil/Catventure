﻿using Gameplay.Systems.Managers;
using Gameplay.Systems.Quests;
using UnityEngine;

namespace Gameplay.Characters
{
    public class GnomeNpc : MonoBehaviour
    {
        public string[] initialDialogue =
        {
            "Not another one of those annoying cats.",
            "What led you here, fur ball?",
            "...",
            "Oh, I see. Well, I can help you, but I'd need a small favor in return.",
            "Do you see that big apple tree over there? I really need some of those juicy apples, because my stupid grandma needs a birthday cake.",
            "I'm old and beginning to rust and besides, heights are not really my thing... So we got a deal?"
        };

        public string[] repeatingDialogueDuringQuest =
        {
            "I need 5 apples from that big apple tree over there.",
        };
        
        public string[] questCompleteDialogue =
        {
            "Ugh, finally. That took you long enough.",
            "...",
            "Oh, right, you wanted some guidance.",
            "Okay listen up kiddo, I don't know what brought you here, but this place is not what you think.",
            "I'd suggest talking to some animals around here, they know what's going on.",
            "Why don't you go down to the lake over there and see if you find anyone."
        };

        public string[] repeatingDialogueAfterQuest =
        {
            "I suggest going to the lake and talking to somebody.",
            "Get out of my face, fur ball"
        };

        private AppleCollectorQuest _appleCollectorQuest;
        private DialogueManager _dialogueManager;

        private void Start()
        {
            _appleCollectorQuest = FindObjectOfType<AppleCollectorQuest>();
            _dialogueManager = FindObjectOfType<DialogueManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWithGnome();
            }
        }

        private void InteractWithGnome()
        {
            if (!_appleCollectorQuest.questStarted)
            {
                StartDialogue(initialDialogue);
                _appleCollectorQuest.StartQuest();
            }
            else if (_appleCollectorQuest.questStarted && !_appleCollectorQuest.questCompleted)
            {
                if (_appleCollectorQuest.GetAppleCount() >= AppleCollectorQuest.TotalApplesRequired)
                {
                    CompleteQuest();
                }
                else
                {
                    StartDialogue(repeatingDialogueDuringQuest);
                }
            }
            else if (_appleCollectorQuest.questCompleted)
            {
                StartDialogue(repeatingDialogueAfterQuest);
            }
        }
        
        private void StartDialogue(string[] dialogueLines)
        {
            _dialogueManager.StartDialogue(dialogueLines);
        }

        private void CompleteQuest()
        {
            StartDialogue(questCompleteDialogue);
            _appleCollectorQuest.CompleteQuest();
        }
    }
}