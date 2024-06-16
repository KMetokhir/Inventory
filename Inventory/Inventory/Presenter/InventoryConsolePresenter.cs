using System.Collections.Generic;

namespace Inventory
{
    internal class InventoryConsolePresenter : IInventoryPresenter
    {
        private readonly IInventoryModel _model;
        private readonly IInventoryView _view;

        public bool IsEmpty => _model.IsEmpty;

        public InventoryConsolePresenter(IInventoryModel model, IInventoryView view)
        {
            _model = model;
            _view = view;

            _view.LoadSlots(CreateSlotsView(_model));

            _model.SlotChanged += OnSlotChanged;
            _model.SlotAdded += OnSlotAdded;
            _model.SlotRemoved += OnSlotRemoved;
        }

        public void ShowItems()
        {
            _view.DisplayInventory();
        }

        public void AddItem(IItem item, uint count)
        {
            _model.AddItem(item, count);
        }

        public bool TryGetItems(uint id, uint Count, out IItem item)
        {
            return _model.TryGetItems(id, Count, out item);
        }

        private void OnSlotRemoved(uint id)
        {
            _view.RemoveSlot(id);
            _view.DisplayInventory();
        }

        private void OnSlotAdded(ISlotInfo slotInfo)
        {
            _view.AddSlot(CreateSlotView(slotInfo.Id, slotInfo.ItemName, slotInfo.Count));
            _view.DisplayInventory();
        }

        private void OnSlotChanged(ISlotInfo slotInfo)
        {
            _view.ChangeSlotCount(slotInfo.Id, slotInfo.Count);
            _view.DisplayInventory();
        }

        private List<ISlotView> CreateSlotsView(IInventoryModel model)
        {
            IEnumerable<ISlotInfo> slots = model.GetSlotsInfo();
            List<ISlotView> slotsView = new List<ISlotView>();

            foreach (ISlotInfo slot in slots)
            {
                slotsView.Add(CreateSlotView(slot.Id, slot.ItemName, slot.Count));
            }

            return slotsView;
        }

        private ISlotView CreateSlotView(uint id, string name, uint count)
        {
            return new SlotConsoleView(id, name, count);
        }
    }
}