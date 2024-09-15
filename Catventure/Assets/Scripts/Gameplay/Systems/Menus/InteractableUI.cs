using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Systems.Menus
{
    public class InteractableUI : MonoBehaviour
    {
        public Camera mainCamera;  // Referenz zur Kamera
        public Transform parentObject;  // Referenz zum Parent-Objekt, zu dem es immer ein Stück daneben bleibt
        public Vector3 worldOffset = new Vector3(2, 2, 0);  // Fester Offset in Weltkoordinaten
        public string displayString = "E";
        public Text textDisplay;

        private void Start()
        {
            // Kamera-Setup: Überprüfe, ob eine Kamera zugewiesen ist, falls nicht, nutze die Hauptkamera
            if (mainCamera == null)
            {
                mainCamera = Camera.main;  // Versuche, die Main Camera zuzuweisen
            }

            // Falls keine Main Camera gefunden wurde, versuche, irgendeine Kamera zu finden
            if (mainCamera == null)
            {
                mainCamera = FindObjectOfType<Camera>();
            }

            // Wenn keine Kamera gefunden wird, gebe eine Fehlermeldung aus
            if (mainCamera == null)
            {
                Debug.LogError("Keine Kamera in der Szene gefunden!");
                return;
            }

            // Stelle sicher, dass ein Parent-Objekt zugewiesen wurde
            if (parentObject == null)
            {
                Debug.LogError("Kein Parent-Objekt zugewiesen!");
                return;
            }

            // Setze die initiale Position des UI-Objekts basierend auf dem Offset
            transform.position = parentObject.position + worldOffset;
        
            //Schreibe den richtigen Text in das Feld
            textDisplay.text = displayString;
        }

        private void Update()
        {
            // Aktualisiere die Position des Child-Objekts, basierend auf der aktuellen Position des Parent-Objekts
            if (parentObject)
            {
                // Setze die Position relativ zur aktuellen Weltposition des Parent-Objekts mit einem festen Offset
                transform.position = parentObject.position + worldOffset;
            }

            // Rotation immer zur Kamera schauen
            if (mainCamera)
            {
                transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward, Vector3.up);
            }
        }
    }
}
