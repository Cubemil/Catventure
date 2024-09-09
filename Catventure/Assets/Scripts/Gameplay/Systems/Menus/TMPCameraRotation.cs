using UnityEngine;

namespace Gameplay.Systems.Menus
{
    public class TMPCameraRotation : MonoBehaviour
    {
        public new Camera camera;
        public Transform objectToRotate;

        private void Update()
        {
            RotateCamera();
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
    }
}