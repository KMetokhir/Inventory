namespace Inventory
{
    internal interface IInventoryPresenter
    {
        bool IsEmpty { get; }

        void ShowItems();
        void AddItem(IItem item, uint count);
        bool TryGetItems(uint id, uint Count, out IItem item);
    }
}