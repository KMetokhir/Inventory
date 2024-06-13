namespace Inventory
{
    internal interface IItem
    {
        string Name { get; }
        IItem Clone();
    }
}