using TMPro;
using UnityEngine;
using System.Linq;
using Gameplay.Systems.Managers;

namespace Gameplay.Systems.Quests
{
    public class AppleCollectorQuest : MonoBehaviour, IQuest
    {
        [SerializeField]
        public TextMeshProUGUI questLogText;
        public const int TotalApplesRequired = 5;
        public bool questStarted;
        private bool _questCompleted;
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
            _questCompleted = true;
            questLogText.gameObject.SetActive(false); // hide quest log once quest is completed
        }

        public int GetAppleCount()
        {
            return inventory.slots.
                Where(slot => slot.itemStack?
                    .GetItem() != null && slot.itemStack.GetItem().id == AppleItemId)
                    .Sum(slot => slot.itemStack.count);
        }

        public void RemoveApplesFromInventory()
        {
            inventory.DeleteItem(AppleItemId, TotalApplesRequired);
        }

        public bool IsQuestCompleted()
        {
            return _questCompleted;
        }
    }
}