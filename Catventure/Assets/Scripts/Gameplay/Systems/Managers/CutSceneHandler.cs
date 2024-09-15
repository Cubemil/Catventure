using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Gameplay.Systems.Managers
{
    public class CutSceneHandler : MonoBehaviour
    {
        public GameObject virtualCameraOne;
        public GameObject virtualCameraTwo;

        private void Update()
        {
            StartCoroutine(SwitchActiveCamera(0f));
            StartCoroutine(DelayedSceneSwitch(5f));
        }
    
        private static IEnumerator DelayedSceneSwitch(float delay)
        {
            yield return new WaitForSeconds(delay); // Wait for the specified delay
            SceneManager.LoadScene("Apartment");
        }
    
        private IEnumerator SwitchActiveCamera(float delay)
        {
            yield return new WaitForSeconds(delay); // Wait for the specified delay
            virtualCameraOne.SetActive(true);
        }
    }
}
