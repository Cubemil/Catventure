using System.Collections;
using UnityEngine;
using Gameplay.Systems.Managers;
using Gameplay.Systems.Quests;
using TMPro;

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
            "Do you see that big apple tree over there? I really need some of those juicy apples.",
            "Why? Well, because my stupid grandma needs a birthday cake.",
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
        private bool _playerInRange;
        private Transform _playerTransform;
        private const float RotationSpeed = 5f;
        public TextMeshProUGUI interactText;
        private Coroutine _rotationCoroutine;

        private void Start()
        {
            _appleCollectorQuest = FindObjectOfType<AppleCollectorQuest>();
            _dialogueManager = FindObjectOfType<DialogueManager>();
            _playerTransform = GameObject.FindWithTag("Player").transform;
            
            interactText.gameObject.SetActive(false);
            interactText.text = "Press 'E' to speak to Garry Gnome";
        }

        private void Update()
        {
            if (_playerInRange && Input.GetKeyDown(KeyCode.E) && !_dialogueManager.IsDialogueActive())
            {
                InteractWithGnome();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag($"PlayerInteract")) return;
            
            _playerInRange = true;
            interactText.gameObject.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag($"PlayerInteract")) return;
            
            _playerInRange = false;
            interactText.gameObject.SetActive(false);
        }

        private void InteractWithGnome()
        {
            // start coroutine to smoothly turn towards the player
            if (_rotationCoroutine != null) StopCoroutine(_rotationCoroutine); // stop ongoing rotation
            _rotationCoroutine = StartCoroutine(LookAtPlayerCoroutine());

            switch (_appleCollectorQuest.questStarted)
            {
                case false:
                {
                    StartDialogue(initialDialogue);
                    _appleCollectorQuest.StartQuest();
                    break;
                }
                case true when !_appleCollectorQuest.questCompleted:
                {
                    if (_appleCollectorQuest.GetAppleCount() >= AppleCollectorQuest.TotalApplesRequired)
                    {
                        _appleCollectorQuest.RemoveApplesFromInventory();
                        _appleCollectorQuest.CompleteQuest();
                        StartDialogue(questCompleteDialogue);
                    }
                    else
                    {
                        StartDialogue(repeatingDialogueDuringQuest);
                    }
                    break;
                }
                default:
                {
                    if (_appleCollectorQuest.questCompleted)
                        StartDialogue(repeatingDialogueAfterQuest);
                    break;
                }
            }
        }
        
        private IEnumerator LookAtPlayerCoroutine()
        {
            var direction = _playerTransform.position - transform.position;
            direction.y = 0; // gnome only rotates horizontally
            var targetRotation = Quaternion.LookRotation(direction);

            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
                yield return null;
            }

            transform.rotation = targetRotation;
        }
        
        private void StartDialogue(string[] dialogueLines)
        {
            _dialogueManager.StartDialogue(dialogueLines);
        }
    }
}