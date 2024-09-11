using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneHandler : MonoBehaviour
{
    public GameObject virtualCameraOne;
    public GameObject virtualCameraTwo;
    
    void Update()
    {
        StartCoroutine(SwitchActiveCamera(0f));
        StartCoroutine(DelayedSceneSwitch(5f));
    }
    
    private IEnumerator DelayedSceneSwitch(float delay)
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
