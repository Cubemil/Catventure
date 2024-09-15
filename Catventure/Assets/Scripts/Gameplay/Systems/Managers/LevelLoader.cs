using System.Collections;
using Gameplay.Systems.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private bool isLoading = false;

    void Start()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        // Check if a scene is currently loading
        if (isLoading)
        {
            return;
        }

        /* For demonstration purposes, trigger loading of the next scene when pressing the space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextLevel();
        } */
    }

    public void LoadNextLevel()
    {
        if (isLoading)
        {
            return;
        }
        
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        isLoading = true; // Set loading flag

        transition.SetTrigger("Start"); // Play animation
        yield return new WaitForSeconds(transitionTime); // Wait for the transition animation to complete

        SceneManager.LoadScene(levelIndex);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isLoading = false; // Scene has finished loading
    }
}