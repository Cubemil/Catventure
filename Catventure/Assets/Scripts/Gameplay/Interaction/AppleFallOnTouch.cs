using UnityEngine;
using Gameplay.Systems.Inventory;

namespace Gameplay.Interaction
{
    public class AppleFallOnTouch : MonoBehaviour
    {
        public KeyCode interactKey = KeyCode.E;
        private Rigidbody _rb;
        private bool _isCollectable = false;
        private bool _hasFallen = false;
        public Inventory inventory;
        public GameObject interactable;
        private const int AppleItemID = 3;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            interactable.SetActive(false);

            // adds rigid body if object doesn't have one
            if (!_rb) _rb = gameObject.AddComponent<Rigidbody>();
            _rb.isKinematic = true;
        }

        private void Update()
        {
            if (_isCollectable && Input.GetKey(interactKey)&& _hasFallen)
            {
                CollectApple();
            }

            interactable.SetActive(_isCollectable && _hasFallen);
        }
        
       private void OnTriggerEnter(Collider other)
       {
           if (other.CompareTag("Player"))
           {
               _isCollectable = true;
           }
       }

       private void OnTriggerExit(Collider other)
       {
           if (other.CompareTag("Player"))
           {
               _isCollectable = false;
           }
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
            if (other.collider.CompareTag("Ground") && !_hasFallen)
            {
                _hasFallen = true;
            }

            if (other.collider.CompareTag("Player") && !_hasFallen)
            {
                DropApple();
            }
        }

        private void CollectApple()
        {
           inventory.SetItemToSlot(new ItemStack(Items.GetItem(AppleItemID), 1));
           gameObject.SetActive(false);
        }
    }
}
