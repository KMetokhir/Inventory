namespace Inventory
{
    internal interface ISlotView
    {
        uint Id { get; }
        string ItemName { get; }
        uint Count { get; }

        void ChangeCount(uint count);
    }
}