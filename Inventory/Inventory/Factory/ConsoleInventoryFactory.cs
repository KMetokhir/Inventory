namespace Inventory
{
    internal class ConsoleInventoryFactory
    {
        public InventoryConsolePresenter CreateInventory(string ownerName)
        {
            InventoryModel model = new InventoryModel();
            InventoryConsoleView view = new InventoryConsoleView(ownerName);

            return new InventoryConsolePresenter(model, view);
        }
    }
}