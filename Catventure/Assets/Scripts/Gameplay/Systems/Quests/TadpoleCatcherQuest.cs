using TMPro;
using UnityEngine;

namespace Gameplay.Systems.Quests
{
    public class TadpoleCatcherQuest : MonoBehaviour
    {
        [SerializeField]
        public TextMeshProUGUI questLogText;
        public const int TadpolesRequired = 3;
        public bool questStarted;
        public bool questCompleted;

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
            {
                questLogText.text = "All tadpoles caught. Return to Froggy in the pond!";
            }
        }

        public void CompleteQuest()
        {
            questCompleted = true;
            questLogText.gameObject.SetActive(false); // hide quest log once quest is completed
        }

    }
}