using UnityEngine;
using Gameplay.Systems.Quests;
using Gameplay.Systems.Inventory;

namespace Gameplay.Interaction
{
    public class AppleFallOnTouch : MonoBehaviour
    {
        [SerializeField] public KeyCode interactKey = KeyCode.E;
        private const int AppleItemID = 3;
        private bool _hasFallen;
        
        private Rigidbody _rb;
        public Inventory inventory;
        public GameObject interactableText;
        private bool _isCollectable;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();

            // adds rigid body if object doesn't have one
            if (!_rb) _rb = gameObject.AddComponent<Rigidbody>();
            _rb.isKinematic = true; // no external gravity effects
        }

        private void Update()
        {
            if (_isCollectable && Input.GetKey(interactKey))
            {
                CollectApple();
            }
        }
        
       private void OnTriggerEnter(Collider other)
       {
           if (other.CompareTag("Player"))
           {
               _hasFallen = true;
               DropApple();
           }

           if (!other.CompareTag($"PlayerInteract") || !_hasFallen) return;
           interactableText.SetActive(true);
           _isCollectable = true;
       }

       private void OnTriggerExit(Collider other)
       {
           if (!other.CompareTag($"PlayerInteract")) return;
           interactableText.SetActive(false);
           _isCollectable = false;
       }

        private void DropApple()
        {
            // allows apple to be affected by gravity -> fall
            _rb.isKinematic = false;
            // prevents potentially falling through the ground
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
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
    }
}
