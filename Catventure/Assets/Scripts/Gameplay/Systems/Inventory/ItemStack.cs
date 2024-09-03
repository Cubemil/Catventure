using System;

namespace Gameplay.Systems.Inventory
{
    [Serializable]
    public class ItemStack
    {
        private ItemClass _item;
        private int _stackSize;
        public int count;

        public ItemStack(ItemClass i, int size)
        {
            _item = i;
            _stackSize = _item.stackSize;
            if (size < _stackSize) count = size;
        }

        public bool IsStackFull()
        {
            return count == _stackSize;
        }

        public ItemStack AddValue(int value)
        {
            if (_item == null) return null;
            
            count += value;
            if (count <= _stackSize) return null;
            
            var i = count - _stackSize;
            count = _stackSize;
            return new ItemStack(_item, i);
        }

        public int DeleteValue(int size)
        {
            if (_item == null || count <= 0) return 0;
            count -= size;
            if (count < 0) return count * -1;
            return 0;
        }

        public ItemClass GetItem()
        {
            return this._item;
        }
    }
}
