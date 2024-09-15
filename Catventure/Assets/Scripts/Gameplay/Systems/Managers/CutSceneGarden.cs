using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneGarden : MonoBehaviour
{
    public GameObject virtualCameraOne;
    public GameObject virtualCameraTwo;
    public GameObject virtualCameraThree;
    public GameObject virtualCameraFour;
    
    void Update()
    {
        StartCoroutine(SwitchActiveCamera(4f));
        StartCoroutine(DelayedSceneSwitch(16f));
    }
    
    private IEnumerator DelayedSceneSwitch(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        SceneManager.LoadScene("Garden");
    }
    
    private IEnumerator SwitchActiveCamera(float delay)
    {
        yield return new WaitForSeconds(0f); // Wait for the specified delay
        virtualCameraTwo.SetActive(true);
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        virtualCameraThree.SetActive(true);
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        virtualCameraFour.SetActive(true);
    }
}