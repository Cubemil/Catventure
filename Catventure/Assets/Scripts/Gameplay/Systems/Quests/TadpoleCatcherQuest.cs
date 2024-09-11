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
        public Inventory.Inventory inventory;
        private const int TadpoleItemId = 420;

        private void Start()
        {
            questLogText.gameObject.SetActive(false); // Hide quest log initially
        }

        public void StartQuest()
        {
            questLogText.gameObject.SetActive(true);
            UpdateQuestLog();
            questStarted = true;
        }

        public void UpdateQuestLog()
        {
            var tadpolesCaught = GetTadpoleCount();
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

        public int GetTadpoleCount()
        {
            var totalTadpoles = 0;

            foreach (var slot in inventory.slots)
            {
                if (slot.itemStack?.GetItem() != null && slot.itemStack.GetItem().id == TadpoleItemId)
                {
                    totalTadpoles += slot.itemStack.count;
                }
            }

            return totalTadpoles;
        }

        public void RemoveTadpolesFromInventory()
        {
            inventory.DeleteItem(TadpoleItemId, TadpolesRequired);
        }
    }
}