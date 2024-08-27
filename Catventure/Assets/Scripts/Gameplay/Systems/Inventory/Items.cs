using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Systems.Inventory;

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
    public static List<ItemCreate> it = new List<ItemCreate>();

    private void OnEnable()
    {
        it = items;
    }

    private void Start()
    {
        it = items;
    }

    public static ItemClass getItem(int id)
    {
        for (int i = 0; i < it.Count; i++)
        {
            var ii = it[i];
            if (ii.id == id)
            {
                return new ItemClass(ii.itemName, ii.description, ii.icon, ii.stackSize, ii.id);
            }
        }
        return null;
    }
}
