using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[Serializable]
public class ItemStack
{
    public ItemClass item;
    public int stacksize;
    public int count;

    public ItemStack(ItemClass i, int size)
    {
        item = i;
        stacksize = item.stackSize;
        if (size < stacksize) count = size;
        
    }

    public bool stackFull()
    {
        return count == stacksize;
    }

    public ItemStack addValue(int value)
    {
        if (item != null)
        {
            count += value;
            if (count > stacksize)
            {
                int i = count - stacksize;
                count = stacksize;
                return new ItemStack(item, i);
            }
        }
        return null;
    }

    public int deleteValue(int size)
    {
        if (item != null)
        {
            if (count > 0)
            {
                count -= size;
                if (count < 0) return count * -1;
            }
        }

        return 0;
    }
}
