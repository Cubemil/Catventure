using UnityEngine;
using UnityEngine.UI;
using Gameplay.Characters;
using Gameplay.Systems.Inventory;

namespace Gameplay.MiniGames.Fishing
{
    public class FishingScript : MonoBehaviour
    {
        // Einstellungen für den Fisch
        public RectTransform fishTransform;
        public float moveSpeed = 100f;
        private float _targetX;

        // Einstellungen für den Slider und den Grünen Handler
        public Slider fishingSlider;
        public RectTransform hookTransform;
        public Slider progressBar;
        public float fillSpeed = 0.1f; // 0.1 pro Sekunde

        // Einstellungen für die Sliderbewegung 
        public float fallSpeed = 0.3f; // Konstante Geschwindigkeit beim Herunterfallen
        public float raiseSpeed = 0.5f; // Geschwindigkeit beim Hochbewegen

        //Starten und beenden des Minispiels
        public GameObject fishingUI;
        public Inventory inventory;

        private bool _isFishingActive;

        private void Start()
        {
            SetNewTargetPosition();
            progressBar.value = 0f;
            fishingUI.SetActive(false);
        }

        private void Update()
        {
            if (!_isFishingActive) return;
            
            // Bewegung des Fisches
            MoveFish();

            // Kontrolle des Sliders
            ControlSlider();

            // Füllen der Progressbar, wenn der Fisch hinter dem Grünen Handler ist
            UpdateProgressBar();

            CheckIfWin();
        }

        public void StartFishing()
        {
            fishingUI.SetActive(true);
            progressBar.value = 0f;
            _isFishingActive = true;
            SetNewTargetPosition();
        }
        
        private void MoveFish()
        {
            // Bewege den Fisch zur Zielposition
            fishTransform.anchoredPosition = 
                Vector2.MoveTowards(
                    fishTransform.anchoredPosition, 
                    new Vector2(_targetX, fishTransform.anchoredPosition.y),
                    moveSpeed * Time.deltaTime
                );

            // Wenn der Fisch das Ziel erreicht hat, setze eine neue Zielposition
            if (Mathf.Approximately(fishTransform.anchoredPosition.x, _targetX))
                SetNewTargetPosition();
        }

        private void SetNewTargetPosition()
        {
            _targetX = Random.Range(-300f, 300f);
        }

        private void ControlSlider()
        {
            // Hol die aktuelle Position des hookTransform (Image im Slider)
            var position = hookTransform.anchoredPosition;

            if (Input.GetMouseButton(0)||Input.GetKey(KeyCode.Space)) // Linke Maustaste gedrückt
            {
                position.x += raiseSpeed * Time.deltaTime * 100; // Bewegung nach rechts
            }
            else
            {
                position.x -= fallSpeed * Time.deltaTime * 100; // Bewegung nach links
            }

            // Begrenze die Position des hookTransform
            position.x = Mathf.Clamp(position.x, -245f, 245f);

            // Setze die neue Position des hookTransform
            hookTransform.anchoredPosition = position;
        }

        private void UpdateProgressBar()
        {
            // Hol die obere und untere Kante des Fisches
            var fishTop = fishTransform.anchoredPosition.x + (fishTransform.rect.width / 4);
            var fishBottom = fishTransform.anchoredPosition.x - (fishTransform.rect.width / 4);

            // Hol die obere und untere Kante des Hakens
            var hookTop = hookTransform.anchoredPosition.x + (hookTransform.rect.width / 2);
            var hookBottom = hookTransform.anchoredPosition.x - (hookTransform.rect.width / 2);

            // Checke, ob der Fisch im Bereich des Hakens ist
            if (fishTop <= hookTop && fishBottom >= hookBottom)
            {
                // Der Fisch ist innerhalb des Hakens → Fülle die Progressbar
                progressBar.value += fillSpeed * Time.deltaTime;
            }
            else
            {
                // Der Fisch ist nicht innerhalb des Hakens → Senke die Progressbar
                progressBar.value -= fillSpeed * Time.deltaTime;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void CheckIfWin()
        {
            if (!progressBar.value.Equals(1f)) return;
            //inventory.SetItemToSlot(new ItemStack(Items.GetItem(5),1));

            fishingUI.SetActive(false);
            _isFishingActive = false;
            progressBar.value = 0f;
            
            // notify FrogNpc that a tadpole was caught
            FindObjectOfType<FrogNpc>().OnTadPoleCaught();
        }
    }
}
