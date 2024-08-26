using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FishingScript : MonoBehaviour
{
    // Einstellungen für den Fisch
    public RectTransform fishTransform;
    public float moveSpeed = 100f;
    private float targetX;

    // Einstellungen für den Slider und den Grünen Handler
    public Slider FishingSlider;
    public RectTransform hookTransform;
    public Slider progressbar;
    public float fillSpeed = 0.1f; // 0.1 pro Sekunde

    // Einstellungen für die Sliderbewegung
    public float fallSpeed = 0.3f; // Konstante Geschwindigkeit beim Herunterfallen
    public float raiseSpeed = 0.5f; // Geschwindigkeit beim Hochbewegen

    //Starten und beenden des Minispiels
    public GameObject FishingUI;
    
    void Start()
    {
        SetNewTargetPosition();
        progressbar.value = 0f;
    }

    void Update()
    {
        if (FishingUI.activeSelf)
        {
            // Bewegung des Fisches
            MoveFish();

            // Kontrolle des Sliders
            ControlSlider();

            // Füllen der Progressbar, wenn der Fisch hinter dem Grünen Handler ist
            UpdateProgressBar();

            CheckIfWin();
        }

    }

    void MoveFish()
    {
        // Bewege den Fisch zur Zielposition
        fishTransform.anchoredPosition = Vector2.MoveTowards(fishTransform.anchoredPosition, new Vector2(targetX, fishTransform.anchoredPosition.y), moveSpeed * Time.deltaTime);

        // Wenn der Fisch das Ziel erreicht hat, setze eine neue Zielposition
        if (Mathf.Approximately(fishTransform.anchoredPosition.x, targetX))
        {
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        targetX = Random.Range(-300f, 300f);
    }

    void ControlSlider()
    {
        // Hol die aktuelle Position des hookTransform (Image im Slider)
        Vector2 position = hookTransform.anchoredPosition;

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



    void UpdateProgressBar()
    {
        // Hol die obere und untere Kante des Fisches
        float fishTop = fishTransform.anchoredPosition.x + (fishTransform.rect.width / 4);
        float fishBottom = fishTransform.anchoredPosition.x - (fishTransform.rect.width / 4);

        // Hol die obere und untere Kante des Hakens
        float hookTop = hookTransform.anchoredPosition.x + (hookTransform.rect.width / 2);
        float hookBottom = hookTransform.anchoredPosition.x - (hookTransform.rect.width / 2);

        // Check ob der Fisch im Bereich des Hakens ist
        if (fishTop <= hookTop && fishBottom >= hookBottom)
        {
            // Der Fisch ist innerhalb des Hakens -> Fülle die Progressbar
            progressbar.value += fillSpeed * Time.deltaTime;
        }
        else
        {
            // Der Fisch ist nicht innerhalb des Hakens -> Senke die Progressbar
            progressbar.value -= fillSpeed * Time.deltaTime;
        }
    }
    
    void CheckIfWin()
    {
        if (progressbar.value.Equals(1f))
        {
            //TODO Funktion für Inventar Fisch hinzugeben aktivieren


            FishingUI.SetActive(false);
        }
    }
}
