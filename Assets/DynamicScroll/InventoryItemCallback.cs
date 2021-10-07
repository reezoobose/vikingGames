using System;
using Inventory;

namespace DynamicScroll
{
    public static class InventoryItemCallback
    {
        public static EventHandler<InventoryItemCallbackArgs> OnItemClicked;
    }

    public class InventoryItemCallbackArgs : EventArgs
    {
        public InventoryItemData InventoryItemData { get; private set; }
        public InventoryItem InventoryItemObject { get; private set; }

        public InventoryItemCallbackArgs(InventoryItemData selectedData, InventoryItem selectedObject)
        {
            InventoryItemData = selectedData;
            InventoryItemObject = selectedObject;
        }
    }
}
