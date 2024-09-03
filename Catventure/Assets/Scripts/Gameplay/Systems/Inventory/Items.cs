using System;
using System.Collections.Generic;
using UnityEngine;

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
        public List<ItemCreate> items = new List<ItemCreate>();
        private static List<ItemCreate> _it = new List<ItemCreate>();

        private void OnEnable()
        {
            _it = items;
        }

        private void Start()
        {
            _it = items;
        }

        public static ItemClass GetItem(int id)
        {
            foreach (var item in _it)
            {
                if (item.id == id)
                {
                    return new ItemClass(item.itemName, item.description, item.icon, item.stackSize, item.id);
                }
            }

            return null;
        }
    }
}