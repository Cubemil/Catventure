using System.Collections;
using Gameplay.Systems.Inventory;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Gameplay.MiniGames
{
    public class QuickTimeEvent : MonoBehaviour
    {
        public Image backgroundCircle; // Der Hintergrundkreis
        public Image successZone; // Der Erfolgsbereich
        public Image indicator; // Der rotierende Indikator
        public KeyCode keyToPress = KeyCode.Space; // Die Taste, die gedrückt werden muss
        public float indicatorSpeed = 500f; // Geschwindigkeit des rotierenden Indikators
        public Text buttonDisplay;
        public GameObject successFeedback;
        public GameObject failureFeedback;
        public GameObject game;
        public Inventory inventory;
    
        private bool _isInSuccessZone = false; // Ob sich der Indikator im Erfolgsbereich befindet
        private int _winCounter;
        private const float TimeSpeed = 0.2f;
        private float _indSpeed;

        private void Start()
        {
            buttonDisplay.text = keyToPress.ToString().ToUpper();
        }

        private void OnEnable()
        {
            StartCoroutine(StartQTE());
            _winCounter = 0;
            // Verwende UnityEngine.Random, um eine zufällige Rotation zu setzen
            var z = Random.Range(0f, 360f);
            indicator.transform.rotation = Quaternion.Euler(0, 0, z);
        
            NewSuccessZone();
        }

        private void Update()
        {
            // Rotiert den Indikator um den Hintergrundkreis
            RotateIndicator();

            // Überprüft, ob die Taste gedrückt wurde
            if (Input.GetKeyDown(keyToPress))
            {
                if (_isInSuccessZone)
                {
                    Success();
                }
                else
                {
                    Fail();
                }
                NewSuccessZone();
            }
        }

        private void RotateIndicator()
        {
            // Rotiert den Indikator
            indicator.transform.RotateAround(backgroundCircle.transform.position, Vector3.forward, _indSpeed * Time.deltaTime);

            // Überprüft, ob der Indikator den Erfolgsbereich erreicht hat
            var angle = Vector3.Angle(indicator.transform.up, successZone.transform.up);
            _isInSuccessZone = angle < 22f; // Setze einen Winkelbereich als Erfolgsbereich
        }

        private IEnumerator StartQTE()
        {
            Time.timeScale=TimeSpeed;
            _indSpeed = indicatorSpeed / TimeSpeed;
            yield return new WaitForSeconds(0.2f); // Startverzögerung
        }

        private void Success()
        {
            _winCounter++;
            if (_winCounter == 3)
            {
                WinGame();
            }
            else
            {
                successFeedback.SetActive(true);
                StartCoroutine(DeactivateFeedback(successFeedback, 0.05f)); // Coroutine starten
            }
        
        }

        private void Fail()
        {
            if (_winCounter != 0)
            {
                _winCounter--;
            }
            failureFeedback.SetActive(true);
            StartCoroutine(DeactivateFeedback(failureFeedback, 0.05f)); // Coroutine starten
        }


        private void NewSuccessZone()
        {
            var z = Random.Range(0f, 360f);
            successZone.transform.rotation = Quaternion.Euler(0, 0, z);
        }
    
        private static IEnumerator DeactivateFeedback(GameObject feedbackObject, float delay)
        {
            yield return new WaitForSeconds(delay);
            feedbackObject.SetActive(false);
        }


        private void WinGame()
        {
            inventory.SetItemToSlot(new ItemStack(Items.GetItem(17), 1));
            EndGame();
        }

        private void EndGame()
        {
            game.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
