using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Gameplay.Interaction
{
    public class CatNipInteractable : ItemInteractable
    {
        public GameObject cat;
        private Animator _catAnimator;
        private static readonly int IsSleeping = Animator.StringToHash("isSleeping");

        void Start()
        {
            _catAnimator = cat.GetComponent<Animator>();
        }

        public override void Interact()
        {
            _catAnimator.SetBool(IsSleeping, true);
            StartCoroutine(DelayedSceneChange(5f)); // Start the coroutine with a 5-second delay
        }
        
        private IEnumerator DelayedSceneChange(float delay)
        {
            yield return new WaitForSeconds(delay); // Wait for the specified delay
            SceneManager.LoadScene("Garden"); // Change the scene after the delay
        }
    }
}