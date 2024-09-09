using TMPro;
using UnityEngine;

namespace Gameplay.Systems.Quests
{
    public class AppleCollectorQuest : MonoBehaviour
    {
        [SerializeField]
        public TextMeshProUGUI questLogText;
        public const int TotalApplesRequired = 5;
        public bool questStarted;
        public bool questCompleted;
        public Inventory.Inventory inventory;
        private const int AppleItemId = 3;

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
            var applesCollected = GetAppleCount();
            questLogText.text = $"Collect Apples ({applesCollected}/{TotalApplesRequired}) for Garry Gnome";

            if (applesCollected >= TotalApplesRequired)
            {
                questLogText.text = "All apples collected. Return to Garry Gnome!";
            }
        }

        public void CompleteQuest()
        {
            questCompleted = true;
            questLogText.gameObject.SetActive(false); // hide quest log once quest is completed
        }

        public int GetAppleCount()
        {
            var totalApples = 0;

            foreach (var slot in inventory.slots)
            {
                if (slot.itemStack?.GetItem() != null && slot.itemStack.GetItem().id == AppleItemId)
                {
                    totalApples += slot.itemStack.count;
                }
            }

            return totalApples;
        }

        public void RemoveApplesFromInventory()
        {
            inventory.DeleteItem(AppleItemId, TotalApplesRequired);
        }
    }
}