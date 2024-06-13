namespace Inventory
{
    internal interface ISlotInfo
    {
        uint Id { get; }
        string ItemName { get; }
        uint Count { get; }
    }
}
