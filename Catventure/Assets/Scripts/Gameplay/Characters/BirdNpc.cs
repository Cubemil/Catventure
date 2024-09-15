using UnityEngine;
using Cinemachine;
using System.Collections;
using Gameplay.Systems.Quests;
using Gameplay.Systems.Managers;
using UnityEngine.SceneManagement;

namespace Gameplay.Characters
{
    public class BirdNpc : MonoBehaviour
    {
        private readonly string[] _questNotAvailableDialogue =
        {
            "Not yet, come back when you're done."
        };
        
        private readonly string[] _initialDialogue =
        {
            "Hey!! You There!!!",
            "Wanna go flying??",
            "...",
            "Oh, you wanna go home??",
            "Sure thing! I'll show you the way."
        };

        private readonly string[] _repeatingDialogue =
        {
            "Let's go flying! Just press 'F' when you're ready!"
        };
        
        private ReturnToRealWorldQuest _returnToRealWorldQuest;
        private TadpoleCatcherQuest _tadpoleCatcherQuest;
        private DialogueManager _dialogueManager;
        private bool _playerInRange;

        [SerializeField] private Rigidbody catRigidbody;
        
        // camera zooming with Cinemachine
        public CinemachineVirtualCamera virtualCamera;
        private Coroutine _rotationCoroutine;
        private float _originalOrthoSize;
        
        private const float ZoomedInOrthoSize = 7.5f;
        private const float TransitionSpeed = 2f;

        public GameObject speakInteractBubble;
        public GameObject flappyBirdInteractBubble;
        public new Camera camera;

        private bool _hasFlyingStarted;
        private const string FlappyCatSceneName = "FlappyCat";
        
        private void Start()
        {
            _returnToRealWorldQuest = FindObjectOfType<ReturnToRealWorldQuest>();
            _tadpoleCatcherQuest = FindObjectOfType<TadpoleCatcherQuest>();
            _dialogueManager = FindObjectOfType<DialogueManager>();
            
            speakInteractBubble.gameObject.SetActive(false);
            flappyBirdInteractBubble.gameObject.SetActive(false);
            
            // original orthographic size
            _originalOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        }

        private void Update()
        {
            if (_playerInRange) RotateInteractBubbles();
            
            if (_playerInRange && Input.GetKeyDown(KeyCode.E) && !_dialogueManager.IsDialogueActive())
                InteractWithBird();

            if (!_playerInRange || !_tadpoleCatcherQuest.IsQuestCompleted() || !_returnToRealWorldQuest.questStarted) return;
            
            if (Input.GetKeyDown(KeyCode.F) && !_hasFlyingStarted)
                StartFlying();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag($"PlayerInteract")) return;
            
            _playerInRange = true;
            speakInteractBubble.gameObject.SetActive(true);
            if (_returnToRealWorldQuest.questStarted) flappyBirdInteractBubble.gameObject.SetActive(true);
            
            // zoom in
            StartCoroutine(ChangeCameraOrthoSize(ZoomedInOrthoSize));
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag($"PlayerInteract")) return;
            
            _playerInRange = false;
            speakInteractBubble.gameObject.SetActive(false);
            flappyBirdInteractBubble.gameObject.SetActive(false);

            // zoom back out
            StartCoroutine(ChangeCameraOrthoSize(_originalOrthoSize));
        }

        private void InteractWithBird()
        {
            if (!_tadpoleCatcherQuest.IsQuestCompleted())
            {
                StartDialogue(_questNotAvailableDialogue);
                return;
            }

            if (!_returnToRealWorldQuest.questStarted)
            {
                StartDialogue(_initialDialogue);
                _returnToRealWorldQuest.StartQuest();
                flappyBirdInteractBubble.gameObject.SetActive(true);
            }
            else
            {
                StartDialogue(_repeatingDialogue);
            }
        }
        
        private void StartDialogue(string[] dialogueLines)
        {
            _dialogueManager.StartDialogue(dialogueLines);
        }

        private void StartFlying()
        {
            _hasFlyingStarted = true;
            speakInteractBubble.gameObject.SetActive(false);
            flappyBirdInteractBubble.gameObject.SetActive(false);

            // let cat fly upwards
            if (!catRigidbody) return;
            StartCoroutine(FloatCatUp());
            
            // 2 secs wait and scene switch
            StartCoroutine(WaitAndLoadFlappyCatScene(2f));
        }
        
        private IEnumerator FloatCatUp()
        {
            const float duration = 2f;
            const float targetHeight = 10f;

            var startPos = catRigidbody.transform.position;
            var targetPos = startPos + Vector3.up * targetHeight;
            var timeElapsed = 0f;

            while (timeElapsed < duration)
            {
                catRigidbody.transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            catRigidbody.transform.position = targetPos;
        }

        private static IEnumerator WaitAndLoadFlappyCatScene(float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(FlappyCatSceneName);
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
            if (!camera || !speakInteractBubble.transform || !flappyBirdInteractBubble.transform) return;
            
            // match rotation
            speakInteractBubble.transform.rotation = camera.transform.rotation;
            flappyBirdInteractBubble.transform.rotation = camera.transform.rotation;
        }
    }
}