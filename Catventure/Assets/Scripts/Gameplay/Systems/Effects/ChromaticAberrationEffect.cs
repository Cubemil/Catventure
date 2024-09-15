using UnityEngine;
using Gameplay.Movement;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

namespace Gameplay.Systems.Effects
{
    public class ChromaticAberrationEffect : MonoBehaviour
    {
        public PostProcessVolume postProcessingVolume;
        private ChromaticAberration _chromaticAberration;
        public GameObject isometricCatController;
        private IsometricPlayerController _activeController;
        
        private void Start()
        {
            _chromaticAberration = postProcessingVolume.profile.GetSetting<ChromaticAberration>();

            _activeController = isometricCatController.GetComponent<IsometricPlayerController>();
            if (_activeController) _activeController.enabled = false;

            if (_chromaticAberration)
                StartCoroutine(AnimateChromaticAberration());
            else
                Debug.LogWarning("Chromatic Aberration not found in PostProcessingVolume");
        }
        
        private IEnumerator AnimateChromaticAberration()
        {
            for (var i = 0; i < 3; i++)
            {
                yield return ChangeIntensity(0.3f, 1.7f);
                yield return ChangeIntensity(1.7f, 0.3f);
                if (_activeController) _activeController.enabled = true;
            }

            yield return ChangeIntensity(0.3f, 0f);
            _chromaticAberration.intensity.value = 0f;
        }

        private IEnumerator ChangeIntensity(float from, float to)
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
    }
}
