using TMPro;
using UnityEngine;
using Gameplay.Systems.Managers;

namespace Gameplay.Systems.Quests
{
    public class TadpoleCatcherQuest : MonoBehaviour, IQuest
    {
        [SerializeField] public TextMeshProUGUI questLogText;
        
        public const int TadpolesRequired = 3;
        public bool questStarted;
        public bool questCompleted;

        // barriers to push around after quest completion
        [SerializeField] public GameObject barrier1;
        [SerializeField] public GameObject barrier2;

        private void Start()
        {
            questLogText.gameObject.SetActive(false); // Hide quest log initially
        }

        public void StartQuest()
        {
            questLogText.gameObject.SetActive(true);
            UpdateQuestLog(0);
            questStarted = true;
        }

        public void UpdateQuestLog(int tadpolesCaught)
        {
            questLogText.text = $"Catch Tadpoles ({tadpolesCaught}/{TadpolesRequired}) for Froggy";

            if (tadpolesCaught >= TadpolesRequired)
                questLogText.text = "All tadpoles caught. Return to Froggy in the pond!";
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void CompleteQuest()
        {
            questCompleted = true;
            var rb1 = barrier1.GetComponent<Rigidbody>();
            var rb2 = barrier2.GetComponent<Rigidbody>();

            if (rb1 && rb2)
            {
                rb1.isKinematic = false;
                rb2.isKinematic = false;
                
                rb1.AddForce(new Vector3(0, 2f, 1f));
                rb2.AddForce(new Vector3(0, 2f, 1f));
            }
            
            questLogText.gameObject.SetActive(false); // hide quest log once quest is completed
        }

        public bool IsQuestCompleted()
        {
            return questCompleted;
        }
    }
}