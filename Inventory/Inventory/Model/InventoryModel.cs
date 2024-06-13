using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory
{
    internal class InventoryModel : IInventoryModel
    {
        private List<ISlot> _slots = new List<ISlot>();
        private uint _availibleSlotId = 0;

        public event Action<ISlotInfo> SlotChanged;
        public event Action<ISlotInfo> SlotAdded;
        public event Action<uint> SlotRemoved;

        public bool IsEmpty => _slots.Any() == false;

        public void AddItem(IItem item, uint count)
        {
            if (count == 0)
            {
                Console.WriteLine($"Добавлено {count} {item.Name}");
                return;
            }

            if (TryFindItemsSlot(item.Name, out ISlot slot))
            {
                slot.IncreaseCount(count);
                SlotChanged?.Invoke(slot);

                Console.WriteLine();
            }
            else
            {
                ISlot newSlot = CreateSlot(_availibleSlotId, item, count);
                _slots.Add(newSlot);
                _availibleSlotId++;
                SlotAdded?.Invoke(newSlot);
            }
        }

        public IEnumerable<ISlotInfo> GetSlotsInfo()
        {
            return _slots;
        }

        public bool TryGetItem(uint id, uint count, out IItem item)
        {
            bool isSuccess = false;
            item = null;

            if (TryFindItemsSlot(id, out ISlot slot))
            {
                if (slot.TryDecreaseCount(count))
                {
                    isSuccess = true;
                    item = slot.Item;

                    if (count == 0)
                    {
                        return isSuccess;
                    }

                    if (slot.Count == 0)
                    {
                        SlotRemoved?.Invoke(slot.Id);
                        _slots.Remove(slot);
                    }
                    else
                    {
                        SlotChanged?.Invoke(slot);
                    }
                }
            }
            else
            {
                Console.WriteLine("Не удалось получить предмет по ID");
            }

            return isSuccess;
        }

        private bool TryFindItemsSlot(string itemName, out ISlot foundSlot)
        {
            foundSlot = _slots.FirstOrDefault(slot => slot.ItemName == itemName);

            return foundSlot != null;
        }

        private bool TryFindItemsSlot(uint id, out ISlot foundSlot)
        {
            foundSlot = _slots.FirstOrDefault(slot => slot.Id == id);

            return foundSlot != null;
        }

        private ISlot CreateSlot(uint id, IItem item, uint count)
        {
            return new Slot(id, item, count);
        }
    }
}