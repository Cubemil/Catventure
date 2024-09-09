using TMPro;
using UnityEngine;

namespace Gameplay.Systems.Quests
{
    public class AppleCollectorQuest : MonoBehaviour
    {
        [SerializeField]
        public TextMeshProUGUI questLogText;
        public const int TotalApplesRequired = 5;
        public bool questStarted = false;
        public bool questCompleted = false;
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

        private void UpdateQuestLog()
        {
            var applesCollected = GetAppleCount();
            questLogText.text = $"Collect Apples ({applesCollected}/{TotalApplesRequired}) for Garry Gnome";

            if (applesCollected >= TotalApplesRequired)
            {
                questLogText.text = questLogText.text = "All apples collected. Return to Garry Gnome!";
            }
        }

        public void CompleteQuest()
        {
            questCompleted = true;
            questLogText.gameObject.SetActive(false); // hide quest log
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
    }
}