using UnityEngine;
using Gameplay.Systems.Quests;
using Gameplay.Systems.Inventory;

namespace Gameplay.Interaction
{
    public class AppleFallOnTouch : MonoBehaviour
    {
        [SerializeField] public KeyCode interactKey = KeyCode.E;

        // only one apple should be collectable (referenced here)
        private static AppleFallOnTouch _activeApple; 
        
        private bool _hasFallen;
        private bool _isCollectable;
        private bool _isTouchingSurface;
        private const int AppleItemID = 3;
        
        private Rigidbody _rb;
        public Inventory inventory;
        public GameObject interactableText;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            interactableText.SetActive(false);

            // adds rigid body if object doesn't have one
            if (!_rb) _rb = gameObject.AddComponent<Rigidbody>();
            _rb.isKinematic = true; // no external gravity effects

            _hasFallen = false;
            _isTouchingSurface = false;
        }

        private void Update()
        {
            if (_activeApple == this && _isCollectable)
            {
                interactableText.SetActive(true);
                if (Input.GetKey(interactKey) && _hasFallen && _isTouchingSurface)
                    CollectApple();
            }
            else
                interactableText.SetActive(false);
        }
        
       private void OnTriggerEnter(Collider other)
       {
           if (!other.CompareTag("Player")) return;
           
           _isCollectable = true;
           SetActiveApple();
       }

       private void OnTriggerExit(Collider other)
       {
           if (!other.CompareTag("Player")) return;
           
           _isCollectable = false;
           if (_activeApple == this)
               _activeApple = null;
       }

       private void DropApple()
        {
            // allows apple to be affected by gravity -> fall
            _rb.isKinematic = false;
            // prevents potentially falling through the ground
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!_hasFallen && !other.collider.CompareTag("Player"))
            {
                _hasFallen = true;
                _isTouchingSurface = true;
            }

            if (other.collider.CompareTag("Player") && !_hasFallen)
                DropApple();
        }

        private void OnCollisionExit(Collision other)
        {
            if (!other.collider.CompareTag("Player"))
                _isTouchingSurface = false;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void CollectApple()
        {
           inventory.SetItemToSlot(new ItemStack(Items.GetItem(AppleItemID), 1));
           
           var appleQuest = FindObjectOfType<AppleCollectorQuest>();
           if (appleQuest && appleQuest.questStarted && !appleQuest.questCompleted)
               appleQuest.UpdateQuestLog();
           
           gameObject.SetActive(false);
        }
        
        private void SetActiveApple()
        {
            if (_activeApple && _activeApple != this)
                _activeApple.interactableText.SetActive(false);
            
            _activeApple = this;
        }
    }
}
