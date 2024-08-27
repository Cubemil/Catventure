using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public Camera camera;
    public Transform objectToRotate;

    void Update()
    {
        // Check if camera and objectToRotate are assigned
        if (camera != null && objectToRotate != null)
        {
            // Option 1: Smooth rotation with adjustable speed
            //float rotationSpeed = 5f; // Adjust rotation speed as needed
            //objectToRotate.rotation = Quaternion.Slerp(objectToRotate.rotation, camera.transform.rotation, rotationSpeed * Time.deltaTime);

            // Option 2: Instant match rotation (if smoothness is not required)
            objectToRotate.rotation = camera.transform.rotation;

            // Optional: Lock rotation to Y axis only (horizontal rotation)
            // Vector3 eulerRotation = objectToRotate.rotation.eulerAngles;
            // eulerRotation.x = 0; // Lock X axis
            // eulerRotation.z = 0; // Lock Z axis
            // objectToRotate.rotation = Quaternion.Euler(eulerRotation);
        }
    }
}