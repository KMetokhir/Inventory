namespace Inventory
{
    internal class Item : IItem
    {
        public Item(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public IItem Clone()
        {
            return new Item(Name);
        }
    }
}