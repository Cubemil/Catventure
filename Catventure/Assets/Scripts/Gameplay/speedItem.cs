using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class SpeedItem : MonoBehaviour
    {
        public GameObject player;
        [FormerlySerializedAs("playerController")] public IsometricPlayerController isometricPlayerController;
        private bool _isSpeedBoostActive = false;

        void Start()
        {
            // Hole die PlayerController-Komponente vom Player-Objekt
            if (player != null)
            {
                isometricPlayerController = player.GetComponent<IsometricPlayerController>();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player && !_isSpeedBoostActive)
            {
                StartCoroutine(SpeedBoost());
            }
        }

        private IEnumerator SpeedBoost()
        {
            _isSpeedBoostActive = true;
            if (isometricPlayerController)
            {
                isometricPlayerController.speed *= 2; // Geschwindigkeit verdoppeln
                gameObject.SetActive(false); // Das Objekt deaktivieren
                yield return new WaitForSeconds(5); // 5 Sekunden warten
                isometricPlayerController.speed /= 2; // Geschwindigkeit zurücksetzen
            }
            _isSpeedBoostActive = false;
            Destroy(gameObject); // Das Objekt endgültig entfernen
        }
    }
}