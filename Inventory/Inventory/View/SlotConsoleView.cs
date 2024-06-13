namespace Inventory
{
    internal class SlotConsoleView : ISlotView
    {
        public SlotConsoleView(uint id, string itemName, uint count)
        {
            Id = id;
            ItemName = itemName;
            Count = count;
        }

        public uint Id { get; private set; }
        public string ItemName { get; private set; }
        public uint Count { get; private set; }

        public void ChangeCount(uint count)
        {
            Count = count;
        }
    }
}