using Gameplay.Systems.Managers;
using UnityEngine;

namespace Gameplay.Systems.Quests
{
    public class AppleCollectorQuest : MonoBehaviour
    {
        public int appleCount = 0;
        public int applesRequired = 0;
        public QuestManager questManager;
        public Inventory.Inventory inventory;

        private void Update()
        {
            foreach (var itemSlot in inventory.slots)
            {
                if (itemSlot.itemStack.GetItem().name == "Apple")
                {
                    appleCount++;
                    
                }
            }
        }
    }
}