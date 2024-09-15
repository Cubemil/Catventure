using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Gameplay.Systems.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public ItemSlot[] slots;
        public Image drag;
        public static ItemStack dragStack;
    
        public Text itemNameTooltip; // text UI-element for item name
        public Text itemDescriptionTooltip; // text UI-element for item description
    
        private bool _dragSet;

        private void Start()
        {
            if (slots == null || slots.Length == 0)
            {
                Debug.LogError("No inventory slots assigned!");
            }
        }

        private void Update()
        {
            if (dragStack != null)
            {
                if (!_dragSet)
                {
                    drag.color = Color.white;
                    drag.sprite = dragStack.GetItem().icon;
                    _dragSet = true;
                }
                drag.transform.position = Input.mousePosition;
            }
            else
            {
                if (_dragSet)
                {
                    drag.color = new Color(0, 0, 0, 0);
                    _dragSet = false;
                }
            }

            //TODO temporarily implemented for testing
            if (Input.GetKeyDown(KeyCode.P))
            {
                SetItemToSlot(new ItemStack(Items.GetItem(1), 1));
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log((DeleteItem(1, 3)));
            }

        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void SetItemToSlot(ItemStack stack)
        {
            if (stack?.GetItem() == null)
            {
                Debug.LogError("Stack or Item in stack is null.");
                return;
            }

            var s = stack;

            for (var i = 0; i < slots.Length; i++)
            {
                var sI = slots[i];

                if (sI == null)
                {
                    Debug.LogError("ItemSlot at index " + i + " is null.");
                    continue;
                }

                if (sI.itemStack is not { count: > 0 })
                {
                    slots[i].itemStack = s;
                    break;
                }
                else if (sI.itemStack.GetItem() != null && sI.itemStack.GetItem().id == stack.GetItem().id)
                {
                    if (sI.itemStack.IsStackFull()) continue;
                    var c = s.count;
                    s = slots[i].itemStack.AddValue(c);
                }
            }
        }

        public bool DeleteItem(int id, int amount)
        {
            var totalSlotsAvailable = 0;
            var slotsToUse = new List<int>();

            for (var i = 0; i < slots.Length; i++)
            {
                var itemSlot = slots[i];
                var slotItem = itemSlot.itemStack.GetItem();
                
                if (!itemSlot || itemSlot.itemStack == null || slotItem == null || slotItem.id != id) continue;
                
                totalSlotsAvailable += itemSlot.itemStack.count;
                slotsToUse.Add(i);
                if (totalSlotsAvailable >= amount) break;
            }

            if (totalSlotsAvailable < amount) return false;

            var remainingAmount = amount;
            foreach (var itemSlot in slotsToUse.Select(slotIndex => slots[slotIndex]))
            {
                if (itemSlot.itemStack.count <= remainingAmount)
                {
                    remainingAmount -= itemSlot.itemStack.count;
                    itemSlot.itemStack = null;
                }
                else
                {
                    itemSlot.itemStack.DeleteValue(remainingAmount);
                    break;
                }
            }

            return true;
        }

        // displays tooltip text on respective slot
        public void ShowTooltip(ItemSlot slot)
        {
            var slotItem = slot.itemStack.GetItem();
            if (slot.itemStack == null || slotItem == null) return;

            itemNameTooltip.text = slotItem.name; // set text to item name
            itemDescriptionTooltip.text = slotItem.description; // set text to item description
            itemNameTooltip.gameObject.SetActive(true);
            itemDescriptionTooltip.gameObject.SetActive(true);
        }

        // hides tooltip
        public void HideTooltip()
        {
            itemNameTooltip.gameObject.SetActive(false);
            itemDescriptionTooltip.gameObject.SetActive(false);
        }
    }
}
