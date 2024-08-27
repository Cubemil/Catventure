using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Systems.Inventory;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;

public class ItemWizzard : ScriptableWizard
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int stackSize;
    public int id;

    [MenuItem("Assets/Items/Item")]
    public static void create()
    {
        ScriptableWizard.CreateWindow<ItemWizzard>();
    }

    private void OnWizardCreate()
    {
        ItemCreate i = ScriptableObject.CreateInstance<ItemCreate>();
        i.itemName = itemName;
        i.description = description;
        i.icon = icon;
        i.stackSize = stackSize;
        i.id = id;
        
        AssetDatabase.CreateAsset(i, "Assets/Items/" + itemName + "_item" + ".asset");
        AssetDatabase.Refresh();
    }
}
