using System.Collections;
using UnityEngine;

namespace Game
{
    public class SpeedItem : MonoBehaviour
    {
        public GameObject player;
        public PlayerController playerController;
        private bool _isSpeedBoostActive = false;

        void Start()
        {
            // Hole die PlayerController-Komponente vom Player-Objekt
            if (player != null)
            {
                playerController = player.GetComponent<PlayerController>();
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
            if (playerController)
            {
                playerController.speed *= 2; // Geschwindigkeit verdoppeln
                gameObject.SetActive(false); // Das Objekt deaktivieren
                yield return new WaitForSeconds(5); // 5 Sekunden warten
                playerController.speed /= 2; // Geschwindigkeit zurücksetzen
            }
            _isSpeedBoostActive = false;
            Destroy(gameObject); // Das Objekt endgültig entfernen
        }
    }
}