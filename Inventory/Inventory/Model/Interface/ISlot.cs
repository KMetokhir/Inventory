namespace Inventory
{
    internal interface ISlot : ISlotInfo
    {
        IItem Item { get; }

        bool TryDecreaseCount(uint value);
        void IncreaseCount(uint value);
    }
}