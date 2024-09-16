using TMPro;
using UnityEngine;
using Gameplay.Movement;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

namespace Gameplay.Interaction
{
    public class CatNipInteractable : ItemInteractable
    {
        public GameObject cat;
        private Animator _catAnimator;
        private static readonly int IsSleeping = Animator.StringToHash("isSleeping");
        private static readonly int IsEating = Animator.StringToHash("isEating");
        public TextMeshProUGUI interactText;

        [SerializeField] private ThirdPersonPlayerController activeController;

        // Post-processing references
        public PostProcessVolume postProcessingVolume;
        private ChromaticAberration _chromaticAberration;
        private Vignette _vignette;

        private void Start()
        {
            // Animator reference
            _catAnimator = cat.GetComponent<Animator>();
            interactText.text = "Press E to try catnip";
            interactText.gameObject.SetActive(false);

            // Get Chromatic Aberration and Vignette from Post-Processing Volume
            _chromaticAberration = postProcessingVolume.profile.GetSetting<ChromaticAberration>();
            _vignette = postProcessingVolume.profile.GetSetting<Vignette>();

            if (!_chromaticAberration || !_vignette)
                Debug.LogWarning("Post-Processing effects (Chromatic Aberration or Vignette) not found.");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerInteract"))
                interactText.gameObject.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PlayerInteract"))
                interactText.gameObject.SetActive(false);
        }

        public override void Interact()
        {
            if (activeController) activeController.canMove = false;
            _catAnimator.SetBool(IsEating, true);
            
            StartCoroutine(DelayedAnimations(3f));
            StartCoroutine(ChromaticAberrationEffect(4f)); // Start Chromatic Aberration pulse
            StartCoroutine(DelayedSceneChange(6f)); // Start the coroutine with a 6-second delay
        }

        private IEnumerator DelayedAnimations(float delay)
        {
            yield return new WaitForSeconds(delay); // Wait for the specified delay
            _catAnimator.SetBool(IsSleeping, true); // Cat lies down to sleep
        }

        private IEnumerator ChromaticAberrationEffect(float delay)
        {
            yield return new WaitForSeconds(delay); // Wait for the specified delay
            // Pulse the chromatic aberration when the cat lies down
            if (!_chromaticAberration) yield break;
            
            for (var i = 0; i < 4; i++)
            {
                yield return ChangeChromaticAberrationIntensity(0.3f, 1.7f);
                yield return ChangeChromaticAberrationIntensity(1.7f, 0.3f);
            }
        }

        private IEnumerator ChangeChromaticAberrationIntensity(float from, float to)
        {
            const float duration = 1.5f;
            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                _chromaticAberration.intensity.value = Mathf.Lerp(from, to, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _chromaticAberration.intensity.value = to;
        }

        private IEnumerator DelayedSceneChange(float delay)
        {
            yield return new WaitForSeconds(delay - 1.5f); // Subtract 1.5 seconds to add vignette effect before scene change

            // Increase vignette intensity before scene switches
            if (_vignette)
                yield return ChangeVignetteIntensity(0.3f, 1.0f); // Smoothly increase vignette to max

            yield return new WaitForSeconds(1.5f); // Wait for vignette effect
            SceneManager.LoadScene("GardenCutScene"); // Change scene
        }

        private IEnumerator ChangeVignetteIntensity(float from, float to)
        {
            const float duration = 1.5f;
            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                _vignette.intensity.value = Mathf.Lerp(from, to, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _vignette.intensity.value = to;
        }
    }
}
