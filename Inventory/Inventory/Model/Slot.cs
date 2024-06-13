namespace Inventory
{
    internal class Slot : ISlot
    {
        private IItem _item;

        public Slot(uint id, IItem item, uint count)
        {
            Id = id;
            _item = item;
            Count = count;
        }

        public uint Id { get; private set; }
        public string ItemName => _item.Name;
        public uint Count { get; private set; }
        public IItem Item => _item.Clone();

        public void IncreaseCount(uint value)
        {
            Count += value;
        }

        public bool TryDecreaseCount(uint value)
        {
            bool isSuccess = false;

            if (Count < value)
            {
                return isSuccess;
            }
            else
            {
                Count -= value;
                isSuccess = true;
            }

            return isSuccess;
        }
    }
}