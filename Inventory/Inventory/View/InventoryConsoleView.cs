using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory
{
    internal class InventoryConsoleView : IInventoryView
    {
        private List<ISlotView> _slotsView = new List<ISlotView>();
        public string _ownerName { get; private set; }

        public InventoryConsoleView(string ownerName)
        {
            _ownerName = ownerName;
        }

        public void LoadSlots(List<ISlotView> slotsView)
        {
            _slotsView = slotsView;
        }

        public void AddSlot(ISlotView slot)
        {
            _slotsView.Add(slot);
        }

        public void ChangeSlotCount(uint id, uint Count)
        {
            if (TryFindSlot(id, out ISlotView slotView))
            {
                slotView.ChangeCount(Count);
            }
        }

        public void RemoveSlot(uint id)
        {
            if (TryFindSlot(id, out ISlotView slotView))
            {
                _slotsView.Remove(slotView);
            }
        }

        public void DisplayInventory()
        {
            Console.WriteLine($"{_ownerName} инвентарь:");

            if (_slotsView.Any() == false)
            {
                Console.WriteLine("пуст...");
            }

            foreach (ISlotView slotView in _slotsView)
            {
                Console.WriteLine($"ID: {slotView.Id}, Название: {slotView.ItemName}, Колличество : {slotView.Count}");
            }

            Console.WriteLine();
        }

        private bool TryFindSlot(uint id, out ISlotView foundSlot)
        {
            foundSlot = _slotsView.FirstOrDefault(slot => slot.Id == id);

            return foundSlot != null;
        }
    }
}