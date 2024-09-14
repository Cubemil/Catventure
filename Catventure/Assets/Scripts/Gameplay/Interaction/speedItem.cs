using UnityEngine;
using Gameplay.Movement;
using System.Collections;

namespace Gameplay.Interaction
{
    public class SpeedItem : MonoBehaviour
    {
        public GameObject playerController;
        public IsometricPlayerController isometricPlayerController;
        private bool _isSpeedBoostActive;

        private void Start()
        {
            if (playerController) isometricPlayerController = playerController.GetComponent<IsometricPlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == playerController && !_isSpeedBoostActive)
                StartCoroutine(SpeedBoost());
        }

        private IEnumerator SpeedBoost()
        {
            _isSpeedBoostActive = true;
            if (isometricPlayerController)
            {
                isometricPlayerController.runSpeed *= 2; // double run speed
                gameObject.SetActive(false); // deactivate this object
                yield return new WaitForSeconds(5); // wait 5 secs
                isometricPlayerController.runSpeed /= 2; // reset run speed
            }
            _isSpeedBoostActive = false;
            Destroy(gameObject); // destroy this obj completely
        }
    }
}