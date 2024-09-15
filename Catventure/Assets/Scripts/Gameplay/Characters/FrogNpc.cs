using UnityEngine;
using Cinemachine;
using System.Collections;
using Gameplay.Systems.Quests;
using Gameplay.Systems.Managers;
using Gameplay.MiniGames.Fishing;

namespace Gameplay.Characters
{
    public class FrogNpc : MonoBehaviour
    {
        private readonly string[] _questNotAvailableDialogue =
        {
            "Hello Cloud, welcome to the lake.",
            "I think you should talk to Garry Gnome, he really needs someone's help right now."
        };
        
        private readonly string[] _initialDialogue =
        {
            "Oh, hello there! It's not every day a cat wanders by here.",
            "It's great that you're here, I could really use some help!",
            "My little frog boys have gone missing. I may have lost my temper a bit and yelled at them.",
            "Now, they've run off and hidden in the pond.",
            "They're good kids, really, but stubborn like their old one.",
            "They won't come out on their own, so I need you to fish them out.",
            "There are five of them in total. Please bring them all back to me.",
            "I'll make it up to you and them, I promise!"
        };

        private readonly string[] _repeatingDialogueDuringQuest =
        {
            "How are you doing with finding the boys?",
            "There are still some hiding in the pond. Hope they dont give you too much trouble.",
        };
        
        private readonly string[] _questCompleteDialogue =
        {
            "You've found all of them! Oh, thank you so much!",
            "I can't tell you how relieved I am.",
            "I know I was hard on them, but they're good kids.",
            "I'll make sure to be a better father.",
            "As for you, Cloud, you're looking for something, aren't you?.",
            "I think you'd find more answers if you head down to the village square.",
            "There are some wise folks there who could point you in the right direction."
        };

        private readonly string[] _repeatingDialogueAfterQuest =
        {
            "Thanks again, Cloud. My boys are safe and sound because of you!",
            "If you're still looking for answers, I really think the village square is your best bet."
        };

        private AppleCollectorQuest _appleCollectorQuest;
        private TadpoleCatcherQuest _tadpoleCatcherQuest;
        private DialogueManager _dialogueManager;
        public GameObject fishingInteractBubble;
        private bool _playerInRange;
        
        // camera zooming with Cinemachine
        public CinemachineVirtualCamera virtualCamera;
        private Coroutine _rotationCoroutine;
        private float _originalOrthoSize;
        
        private const float ZoomedOutOrthoSize = 20f;
        private const float TransitionSpeed = 2f;
        
        public GameObject tadpole1;
        public GameObject tadpole2;
        public GameObject tadpole3;

        public GameObject fishingUI;
        public FishingScript fishingScript;
        private int _caughtTadpoles;
        
        public GameObject interactBubble;
        public new Camera camera;
            
        private void Start()
        {
            _appleCollectorQuest = FindObjectOfType<AppleCollectorQuest>();
            _tadpoleCatcherQuest = FindObjectOfType<TadpoleCatcherQuest>();
            _dialogueManager = FindObjectOfType<DialogueManager>();
            
            interactBubble.gameObject.SetActive(false);
            fishingInteractBubble.gameObject.SetActive(false);
            
            // original orthographic size
            _originalOrthoSize = virtualCamera.m_Lens.OrthographicSize;
            
            tadpole1.SetActive(false);
            tadpole2.SetActive(false);
            tadpole3.SetActive(false);
        }

        private void Update()
        {
            if (_playerInRange) RotateInteractBubbles();
            
            if (_playerInRange && Input.GetKeyDown(KeyCode.E) && !_dialogueManager.IsDialogueActive())
                InteractWithFrog();

            if (!_playerInRange || !_tadpoleCatcherQuest.questStarted || _tadpoleCatcherQuest.IsQuestCompleted()) return;
            
            if (Input.GetKeyDown(KeyCode.F))
                StartFishing();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag($"PlayerInteract")) return;
            
            _playerInRange = true;
            interactBubble.gameObject.SetActive(true);
            
            // zoom out
            StartCoroutine(ChangeCameraOrthoSize(ZoomedOutOrthoSize));
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag($"PlayerInteract")) return;
            
            _playerInRange = false;
            interactBubble.gameObject.SetActive(false);
            fishingInteractBubble.gameObject.SetActive(false);

            // zoom back in
            StartCoroutine(ChangeCameraOrthoSize(_originalOrthoSize));
        }

        private void InteractWithFrog()
        {
            // zoom in only halfway
            StartCoroutine(ChangeCameraOrthoSize(_originalOrthoSize * 1.5f));
            
            if (!_appleCollectorQuest.IsQuestCompleted())
            {
                StartDialogue(_questNotAvailableDialogue);
                return;
            }
            
            switch (_tadpoleCatcherQuest.questStarted)
            {
                case false:
                    StartDialogue(_initialDialogue);
                    _tadpoleCatcherQuest.StartQuest();
                    fishingInteractBubble.gameObject.SetActive(true);
                    break;
                case true when !_tadpoleCatcherQuest.IsQuestCompleted():
                    if (_caughtTadpoles >= TadpoleCatcherQuest.TadpolesRequired)
                    {
                        CompleteFrogQuest();
                    }
                    else
                    {
                        StartDialogue(_repeatingDialogueDuringQuest);
                    }
                    break;
                default:
                    if (_tadpoleCatcherQuest.IsQuestCompleted())
                    {
                        StartDialogue(_repeatingDialogueAfterQuest);
                    }
                    break;
            }
        }
        
        private void StartDialogue(string[] dialogueLines)
        {
            _dialogueManager.StartDialogue(dialogueLines);
        }

        private void StartFishing()
        {
            fishingUI.SetActive(true);
            fishingScript.StartFishing();
            fishingInteractBubble.gameObject.SetActive(false);
        }
        
        private void CompleteFrogQuest()
        {
            _tadpoleCatcherQuest.CompleteQuest();
            StartDialogue(_questCompleteDialogue);
        }
        
        public void OnTadPoleCaught()
        {
            _caughtTadpoles++;
            _tadpoleCatcherQuest.UpdateQuestLog(_caughtTadpoles);

            switch (_caughtTadpoles)
            {
                case 1:
                    tadpole1.SetActive(true);
                    break;
                case 2:
                    tadpole2.SetActive(true);
                    break;
                case 3:
                    tadpole3.SetActive(true);
                    break;
            }

            if (_caughtTadpoles == 3)
            {
                fishingUI.SetActive(false);
                interactBubble.gameObject.SetActive(true);
            }
            else
            {
                fishingInteractBubble.gameObject.SetActive(true);
            }
        }

        // Coroutine to smoothly change the camera's orthographic size 
        private IEnumerator ChangeCameraOrthoSize(float targetOrthoSize)
        {
            var startSize = virtualCamera.m_Lens.OrthographicSize;
            var progress = 0f;

            while (Mathf.Abs(virtualCamera.m_Lens.OrthographicSize - targetOrthoSize) > 0.1f)
            {
                progress += Time.deltaTime * TransitionSpeed;
                virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetOrthoSize, progress);
                yield return null;
            }

            virtualCamera.m_Lens.OrthographicSize = targetOrthoSize;
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void RotateInteractBubbles()
        {
            if (!camera || !interactBubble.transform || !fishingInteractBubble.transform) return;
            
            // match rotation
            interactBubble.transform.rotation = camera.transform.rotation;
            fishingInteractBubble.transform.rotation = camera.transform.rotation;
        }

    }
}