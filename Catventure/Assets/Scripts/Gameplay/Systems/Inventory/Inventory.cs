using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public Image drag;
    public static ItemStack dragStack;
    
    public Text itemNameTooltip; // Text UI Element für den Item-Namen
    public Text itemDescriptionTooltip; // Text UI Element für die Item-Beschreibung
    
    private bool dragset;

    private void Update()
    {
        if (dragStack != null)
        {
            if (!dragset)
            {
                drag.color = Color.white;
                drag.sprite = dragStack.item.icon;
                dragset = true;
            }
            drag.transform.position = Input.mousePosition;
        }
        else
        {
            if (dragset)
            {
                drag.color = new Color(0, 0, 0, 0);
                dragset = false;
            }
        }

        //Todo vorübergehend zur Veranschaulichung und zum Testen
        if (Input.GetKeyDown(KeyCode.P))
        {
            setItemToSlot(new ItemStack(Items.getItem(1), 1));
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log((DeleteItem(1, 3)));
        }

    }

    public void setItemToSlot(ItemStack stack)
    {
        if (stack == null || stack.item == null)
        {
            Debug.LogError("Stack or Item in stack is null.");
            return;
        }

        ItemStack s = stack;

        for (int i = 0; i < slots.Length; i++)
        {
            var sI = slots[i];

            if (sI == null)
            {
                Debug.LogError("ItemSlot at index " + i + " is null.");
                continue;
            }

            if (sI.iStack == null || sI.iStack.count <= 0)
            {
                slots[i].iStack = s;
                break;
            }
            else if (sI.iStack.item != null && sI.iStack.item.id == stack.item.id)
            {
                if (!sI.iStack.stackFull())
                {
                    int c = s.count;
                    s = slots[i].iStack.addValue(c);
                }
            }
        }
    }

    public bool DeleteItem(int id, int amount)
    {
        int totalAvailable = 0;
        List<int> slotsToUse = new List<int>();

        for (int i = 0; i < slots.Length; i++)
        {
            var sI = slots[i];
            if (sI != null && sI.iStack != null && sI.iStack.item != null && sI.iStack.item.id == id)
            {
                totalAvailable += sI.iStack.count;
                slotsToUse.Add(i);
                if (totalAvailable >= amount)
                {
                    break;
                }
            }
        }

        if (totalAvailable < amount)
        {
            return false;
        }

        int remainingAmount = amount;
        foreach (int slotIndex in slotsToUse)
        {
            var sI = slots[slotIndex];
            if (sI.iStack.count <= remainingAmount)
            {
                remainingAmount -= sI.iStack.count;
                sI.iStack = null;
            }
            else
            {
                sI.iStack.deleteValue(remainingAmount);
                remainingAmount = 0;
                break;
            }
        }

        return true;
    }

    // Funktion zum Anzeigen des Tooltips
    public void ShowTooltip(ItemSlot slot)
    {
        if (slot.iStack != null && slot.iStack.item != null)
        {
            itemNameTooltip.text = slot.iStack.item.name; // Setze den Namen des Items
            itemDescriptionTooltip.text = slot.iStack.item.description; // Setze die Beschreibung des Items
            itemNameTooltip.gameObject.SetActive(true);
            itemDescriptionTooltip.gameObject.SetActive(true);
        }
    }

    // Funktion zum Verstecken des Tooltips
    public void HideTooltip()
    {
        itemNameTooltip.gameObject.SetActive(false);
        itemDescriptionTooltip.gameObject.SetActive(false);
    }
}
