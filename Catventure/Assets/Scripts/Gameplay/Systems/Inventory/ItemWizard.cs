#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Gameplay.Systems.Inventory
{
    public class ItemWizard : ScriptableWizard
    {
        public string itemName;
        public string description;
        public Sprite icon;
        public int stackSize;
        public int id;

        [MenuItem("Assets/Items/Item")]
        public static void Create()
        {
            ScriptableWizard.CreateWindow<ItemWizard>();
        }

        private void OnWizardCreate()
        {
            var i = ScriptableObject.CreateInstance<ItemCreate>();
            
            i.itemName = itemName;
            i.description = description;
            i.icon = icon;
            i.stackSize = stackSize;
            i.id = id;
        
            AssetDatabase.CreateAsset(i, "Assets/Items/" + itemName + "_item" + ".asset");
            AssetDatabase.Refresh();
        }
    }
}
#endif
