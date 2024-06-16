using System;
using System.Collections.Generic;

namespace Inventory
{
    internal interface IInventoryModel
    {
        event Action<ISlotInfo> SlotChanged;
        event Action<ISlotInfo> SlotAdded;
        event Action<uint> SlotRemoved;

        bool IsEmpty { get; }

        void AddItem(IItem item, uint count);
        IEnumerable<ISlotInfo> GetSlotsInfo();
        bool TryGetItems(uint id, uint count, out IItem item);
    }
}