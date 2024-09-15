using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Gameplay.Systems.Inventory
{
    [Serializable]
    public class ItemClass
    {
        public string name;
        public string description;
        public Sprite icon;
        public int stackSize;
        public int id;

        public ItemClass(string name, string description, Sprite icon, int stackSize, int id)
        {
            this.name = name;
            this.description = description;
            this.icon = icon;
            this.stackSize = stackSize;
            this.id = id;
        }
    }
    
    public class Items : MonoBehaviour
    {
        public List<ItemCreate> items = new List<ItemCreate>(); // List to hold items
        private static List<ItemCreate> _it = new List<ItemCreate>(); // Static list used to store items at runtime

        private void OnEnable()
        {
            if (items == null || items.Count == 0)
            {
                Debug.LogError("Items list is empty or not assigned in the Inspector.");
            }

            InitializeItems(); // Called when the script is enabled
        }

        private static void InitializeItems()
        {
            if (_it == null || _it.Count == 0)
            {
                var itemsComponent = FindObjectOfType<Items>(); // Find the Items component in the scene
                if (itemsComponent != null)
                {
                    _it = itemsComponent.items; // Assign the list from Items component
                    Debug.Log("Items list initialized with " + _it.Count + " items.");
                }
                else
                {
                    Debug.LogError("Items component not found in the scene!");
                }
            }
        }

        public static ItemClass GetItem(int id)
        {
            InitializeItems(); // Ensures _it is always initialized
            if (_it == null || _it.Count == 0)
            {
                Debug.LogError("Items list is not initialized or empty.");
                return null; // Return null if list is empty
            }

            var item = _it.FirstOrDefault(item => item != null && item.id == id);
            if (item == null)
            {
                Debug.LogError("Item with ID " + id + " not found.");
                return null; // Return null if the item is not found
            }

            return new ItemClass(item.itemName, item.description, item.icon, item.stackSize, item.id); // Return a new instance of ItemClass
        }
    }

}