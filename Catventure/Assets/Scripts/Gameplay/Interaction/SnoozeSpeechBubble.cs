using TMPro;
using UnityEngine;

namespace Gameplay.Interaction
{
    public class SnoozeSpeechBubble : MonoBehaviour
    {
        public new Camera camera;
        public Transform objectToRotate;
        public Animator animator;
        public TMP_Text textInput;

        private float _sleepTimer;
        private int _sleepState; // 0: "Zzzz.", 1: "Zzzz..", 2: "Zzzz..."

        private void Update()
        {
            RotateCamera();
            Sleep();
        }

        private void RotateCamera()
        {
            // Check if camera and objectToRotate are assigned
            if (camera && objectToRotate)
            {
                // Option 2: Instant match rotation (if smoothness is not required)
                objectToRotate.rotation = camera.transform.rotation;
            }
        }

        private void Sleep()
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("CatSleeping"))
            {
                textInput.text = "";
            }
            else 
            {
                _sleepTimer += Time.deltaTime;

                if (!(_sleepTimer >= 0.5f)) return; // Change text every 0.5 seconds
                _sleepTimer = 0f;
                _sleepState = (_sleepState + 1) % 3; // Cycle through 0, 1, 2

                textInput.text = _sleepState switch
                {
                    0 => "Zzzz.",
                    1 => "Zzzz..",
                    2 => "Zzzz...",
                    _ => textInput.text
                };
            }
        }
    }
}