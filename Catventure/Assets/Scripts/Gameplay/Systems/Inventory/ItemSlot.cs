using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public ItemStack iStack = null;
    public Sprite defaultImage;
    public Image icone;
    public Text valueText;
    private bool isSlot, setted;
    private Inventory inventory; // Referenz zu deinem Inventory-Script

    private void Start()
    {
        icone = GetComponent<Image>();
        valueText = transform.GetComponentInChildren<Text>();
        inventory = FindObjectOfType<Inventory>(); // Finden des Inventory-Scripts
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isSlot = true;
        inventory.ShowTooltip(this); // Zeigt den Tooltip an, wenn die Maus darüber schwebt
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isSlot = false;
        inventory.HideTooltip(); // Versteckt den Tooltip, wenn die Maus das Item verlässt
    }

    private void Update()
    {
        if (isSlot)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (iStack != null && iStack.count > 0)
                {
                    Inventory.dragStack = iStack;
                    iStack = null;
                }
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                var iDrag = Inventory.dragStack;
                if (iDrag != null)
                {
                    if (iStack == null)
                    {
                        iStack = iDrag;
                        Inventory.dragStack = null;
                    }
                    else if (iStack.count <= 0)
                    {
                        iStack = iDrag;
                        Inventory.dragStack = null;
                    }
                    else
                    {
                        if (iDrag.item.id == iStack.item.id)
                        {
                            var i = Inventory.dragStack.count;
                            Inventory.dragStack = iStack.addValue(i);
                        }
                    }
                }
            }
        }

        if (iStack != null && iStack.count > 0)
        {
            if (iStack.item.icon != null && !setted)
            {
                icone.sprite = iStack.item.icon;
                setted = true;
            }

            if (!valueText.text.Equals(iStack.count + ""))
            {
                valueText.text = iStack.count + "";
            }
        }
        else
        {
            if (setted)
            {
                icone.sprite = defaultImage;
                valueText.text = "";
                setted = false;
            }
        }
    }
}
