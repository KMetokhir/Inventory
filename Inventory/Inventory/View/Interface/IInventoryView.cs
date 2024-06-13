using System.Collections.Generic;

namespace Inventory
{
    internal interface IInventoryView
    {
        string OwnerName { get; }

        void LoadSlots(List<ISlotView> slotsView);
        void AddSlot(ISlotView slot);
        void ChangeSlotCount(uint id, uint Count);
        void DisplayInventory();
        void RemoveSlot(uint id);
    }
}