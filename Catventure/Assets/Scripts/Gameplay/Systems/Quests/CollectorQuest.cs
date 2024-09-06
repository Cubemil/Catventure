using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Systems.Quests
{
    public class CollectorQuest : MonoBehaviour
    {
        public Inventory.Inventory inventory;
        public string requiredItemName;
        public int requiredQuantity;

        private void Update()
        {
            
        }
    }
}