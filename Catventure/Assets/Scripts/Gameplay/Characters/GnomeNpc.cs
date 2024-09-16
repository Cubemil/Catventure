using UnityEngine;
using System.Collections;
using Gameplay.Systems.Quests;
using Gameplay.Systems.Managers;
using UnityEngine.Serialization;

namespace Gameplay.Characters
{
    public class GnomeNpc : MonoBehaviour
    {
        private readonly string[] _initialDialogue =
        {
            "Not another one of those annoying cats.",
            "What led you here, fur ball?",
            "...",
            "Oh, I see. Well, I can help you, but I'd need a small favor in return.",
            "Do you see that big apple tree over there? I really need some of those juicy apples.",
            "Why? Well, because my stupid grandma needs a birthday cake.",
            "I'm old and beginning to rust and besides, heights are not really my thing... So we got a deal?"
        };

        private readonly string[] _repeatingDialogueDuringQuest =
        {
            "I need 5 apples from that big apple tree over there.",
            "Try jumping on stuff to see if you can get up the tree."
        };
        
        private readonly string[] _questCompleteDialogue =
        {
            "Ugh, finally. That took you long enough.",
            "...",
            "Oh, right, you wanted some guidance.",
            "Okay listen up kiddo, I don't know what brought you here, but this place is not what you think.",
            "I'd suggest talking to some animals around here, they know what's going on.",
            "Why don't you go down to the lake over there and see if you find anyone."
        };

        private readonly string[] _repeatingDialogueAfterQuest =
        {
            "I suggest going to the lake and talking to somebody.",
            "Get out of my face, fur ball"
        };

        private AppleCollectorQuest _appleCollectorQuest;
        private DialogueManager _dialogueManager;
        private bool _playerInRange;
        private Transform _playerTransform;
        private const float RotationSpeed = 5f;
        private Coroutine _rotationCoroutine;

        [SerializeField] public Sprite speechBubbleSprite;
        public GameObject interactBubble;
        public new Camera camera;

        private void Start()
        {
            _appleCollectorQuest = FindObjectOfType<AppleCollectorQuest>();
            _dialogueManager = FindObjectOfType<DialogueManager>();
            _playerTransform = GameObject.FindWithTag("Player").transform;
            
            interactBubble.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_playerInRange) RotateInteractBubble();
            
            if (_playerInRange && Input.GetKeyDown(KeyCode.E) && !_dialogueManager.IsDialogueActive())
                InteractWithGnome();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag($"PlayerInteract")) return;
            
            _playerInRange = true;
            interactBubble.gameObject.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag($"PlayerInteract")) return;
            
            _playerInRange = false;
            interactBubble.gameObject.SetActive(false);
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
                    StartDialogue(_initialDialogue);
                    _appleCollectorQuest.StartQuest();
                    break;
                }
                case true when !_appleCollectorQuest.IsQuestCompleted():
                {
                    if (_appleCollectorQuest.GetAppleCount() >= AppleCollectorQuest.TotalApplesRequired)
                    {
                        _appleCollectorQuest.RemoveApplesFromInventory();
                        _appleCollectorQuest.CompleteQuest();
                        StartDialogue(_questCompleteDialogue);
                    }
                    else
                    {
                        StartDialogue(_repeatingDialogueDuringQuest);
                    }
                    break;
                }
                default:
                {
                    if (_appleCollectorQuest.IsQuestCompleted())
                        StartDialogue(_repeatingDialogueAfterQuest);
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
            _dialogueManager.StartDialogue(dialogueLines, speechBubbleSprite);
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void RotateInteractBubble()
        {
            if (!camera || !interactBubble.transform) return;
            
            // match rotation
            interactBubble.transform.rotation = camera.transform.rotation;
        }
    }
}