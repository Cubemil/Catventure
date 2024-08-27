using UnityEngine;

namespace Gameplay.Systems.Inventory
{
    public class ItemCreate : ScriptableObject
    {
        public string itemName;
        public string description;
        public Sprite icon;
        public int stackSize;
        public int id;
    }
}
