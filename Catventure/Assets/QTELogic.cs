using System.Collections;
using Gameplay.Systems.Inventory;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QTELogic : MonoBehaviour
{
    public Image backgroundCircle; // Der Hintergrundkreis
    public Image successZone; // Der Erfolgsbereich
    public Image indicator; // Der rotierende Indikator
    public KeyCode keyToPress = KeyCode.Space; // Die Taste, die gedrückt werden muss
    public float indicatorSpeed = 500f; // Geschwindigkeit des rotierenden Indikators
    public Text ButtonDisplay;
    public GameObject successFeedback;
    public GameObject failureFeedback;
    public GameObject game;
    public Inventory inv;

    
    private bool isInSuccessZone = false; // Ob sich der Indikator im Erfolgsbereich befindet
    private int winCounter;
    private float TimeSpeed = 0.2f;
    private float indSpeed;


    private void Start()
    {
        ButtonDisplay.text = keyToPress.ToString().ToUpper();
    }

    void OnEnable()
    {
        StartCoroutine(StartQTE());
        winCounter = 0;
        // Verwende UnityEngine.Random, um eine zufällige Rotation zu setzen
        float z = Random.Range(0f, 360f);
        indicator.transform.rotation = Quaternion.Euler(0, 0, z);
        
       newSuccessZone();
    }

    void Update()
    {
        // Rotiert den Indikator um den Hintergrundkreis
        RotateIndicator();

        // Überprüft, ob die Taste gedrückt wurde
        if (Input.GetKeyDown(keyToPress))
        {
            if (isInSuccessZone)
            {
                Success();
            }
            else
            {
                Fail();
            }
            newSuccessZone();
        }
    }

    private void RotateIndicator()
    {
        // Rotiert den Indikator
        indicator.transform.RotateAround(backgroundCircle.transform.position, Vector3.forward, indSpeed * Time.deltaTime);

        // Überprüft, ob der Indikator den Erfolgsbereich erreicht hat
        float angle = Vector3.Angle(indicator.transform.up, successZone.transform.up);
        isInSuccessZone = angle < 22f; // Setze einen Winkelbereich als Erfolgsbereich
    }

    private IEnumerator StartQTE()
    {
        Time.timeScale=TimeSpeed;
        indSpeed = indicatorSpeed / TimeSpeed;
        yield return new WaitForSeconds(0.2f); // Startverzögerung
    }

    private void Success()
    {
        winCounter++;
        if (winCounter == 3)
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
        if (winCounter != 0)
        {
            winCounter--;
        }
        failureFeedback.SetActive(true);
        StartCoroutine(DeactivateFeedback(failureFeedback, 0.05f)); // Coroutine starten
    }


    private void newSuccessZone()
    {
        float z = Random.Range(0f, 360f);
        successZone.transform.rotation = Quaternion.Euler(0, 0, z);
    }
    
    private IEnumerator DeactivateFeedback(GameObject feedbackObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        feedbackObject.SetActive(false);
    }


    private void WinGame()
    {
        inv.SetItemToSlot(new ItemStack(Items.GetItem(17), 1));
        EndGame();
    }

    public void EndGame()
    {
        game.SetActive(false);
        Time.timeScale = 1f;
    }
}
