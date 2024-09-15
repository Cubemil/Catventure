using UnityEngine;

namespace Gameplay.Systems.Menus
{
    public class TMPCameraRotation : MonoBehaviour
    {
        public new Camera camera;
        public Transform objectToRotate;

        private void Update()
        {
            if (!gameObject.activeSelf)
            {
                RotateCamera();
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void RotateCamera()
        {
            // Check if camera and objectToRotate are assigned
            if (!camera || !objectToRotate) return;
            
            // match rotation
            Debug.Log("rotating tmp");
            objectToRotate.rotation = camera.transform.rotation;
        }
    }
}