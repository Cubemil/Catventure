using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedItem : MonoBehaviour
{
    public GameObject player;
    public PlayerControl playerController;
    private bool isSpeedBoostActive = false;

    void Start()
    {
        // Hole die PlayerController-Komponente vom Player-Objekt
        if (player != null)
        {
            playerController = player.GetComponent<PlayerControl>();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !isSpeedBoostActive)
        {
            StartCoroutine(SpeedBoost());
        }
    }

    IEnumerator SpeedBoost()
    {
        isSpeedBoostActive = true;
        if (playerController != null)
        {
            playerController.speed *= 2; // Geschwindigkeit verdoppeln
            gameObject.SetActive(false); // Das Objekt deaktivieren
            yield return new WaitForSeconds(5); // 5 Sekunden warten
            playerController.speed /= 2; // Geschwindigkeit zurücksetzen
        }
        isSpeedBoostActive = false;
        Destroy(gameObject); // Das Objekt endgültig entfernen
    }
}