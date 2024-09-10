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
        private static readonly int IsEating = Animator.StringToHash("isEating");

        void Start()
        {
            _catAnimator = cat.GetComponent<Animator>();
        }

        public override void Interact()
        {
            _catAnimator.SetBool(IsEating, true);
            StartCoroutine(DelayedAnimations(3f));
            StartCoroutine(DelayedSceneChange(6f)); // Start the coroutine with a 5-second delay
        }
        
        private IEnumerator DelayedAnimations(float delay)
        {
            yield return new WaitForSeconds(delay); // Wait for the specified delay
            _catAnimator.SetBool(IsSleeping, true);
        }
        
        private IEnumerator DelayedSceneChange(float delay)
        {
            yield return new WaitForSeconds(delay); // Wait for the specified delay
            SceneManager.LoadScene("Garden"); // Change the scene after the delay
        }
        
        
    }
}