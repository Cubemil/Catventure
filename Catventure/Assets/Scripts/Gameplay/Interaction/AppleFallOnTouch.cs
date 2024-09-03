using System;
using Gameplay.Systems.Inventory;
using UnityEngine;

namespace Gameplay.Interaction
{
    public class AppleFallOnTouch : MonoBehaviour
    {
        public KeyCode interactKey = KeyCode.E;
        private Rigidbody _rb;
        private bool _iscollectable;
        private bool _hasFallen = false;
        public Inventory inv;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();

            // adds rigid body if object doesn't have one
            if (!_rb) _rb = gameObject.AddComponent<Rigidbody>();
            _rb.isKinematic = true;
        }

        private void Update()
        {
            if (_iscollectable && Input.GetKey(interactKey) && _hasFallen)
            {
                collectApple();
            }
        }

        /* private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Collectable");
            }
            
            if (other.CompareTag("Player") && Input.GetKeyDown(interactKey))
            {
                DropApple();
            }
            
            if (other.CompareTag("Player") && _hasFallen)
            {
                collectApple();
            }
        }*/

       private void OnTriggerEnter(Collider other)
       {
           if (other.CompareTag("Player"))
           {
               _iscollectable = true;
           }
       }

       private void OnTriggerExit(Collider other)
       {
           if (other.CompareTag("Player"))
           {
               _iscollectable = false;
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

        //todo
        private void collectApple()
        {
           Debug.Log("collected");
           inv.SetItemToSlot(new ItemStack(Items.GetItem(3), 1));
           gameObject.SetActive(false);
        }
    }
}
