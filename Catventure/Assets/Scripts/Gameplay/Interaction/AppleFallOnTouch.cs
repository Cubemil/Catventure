using System;
using UnityEngine;

namespace Gameplay.Interaction
{
    public class AppleFallOnTouch : MonoBehaviour
    {
        public KeyCode interactKey = KeyCode.E;
        private Rigidbody _rb;
        private bool _hasFallen = false;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();

            // adds rigid body if object doesn't have one
            if (!_rb) _rb = gameObject.AddComponent<Rigidbody>();
            _rb.isKinematic = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") && Input.GetKeyDown(interactKey))
            {
                DropApple();
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
        }
    }
}
