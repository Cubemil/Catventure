using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.Systems.Managers
{
    public class LevelLoader : MonoBehaviour
    {
        public Animator transition;
        public float transitionTime = 1f;
        private bool _isLoading;
        private static readonly int Start1 = Animator.StringToHash("Start");

        private void Start()
        {
            // Subscribe to the sceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            // Unsubscribe from the sceneLoaded event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void Update()
        {
            // Check if a scene is currently loading
            if (_isLoading)
            {
                return;
            }
        }

        public void LoadNextLevel()
        {
            if (_isLoading)
            {
                return;
            }
        
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }

        private IEnumerator LoadLevel(int levelIndex)
        {
            _isLoading = true; // Set loading flag

            transition.SetTrigger(Start1); // Play animation
            yield return new WaitForSeconds(transitionTime); // Wait for the transition animation to complete

            SceneManager.LoadScene(levelIndex);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _isLoading = false; // Scene has finished loading
        }
    }
}