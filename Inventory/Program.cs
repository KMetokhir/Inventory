using System;
using System.Text;

namespace Inventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            ConsoleInventoryFactory consoleInventoryFactory = new ConsoleInventoryFactory();

            string inventoryOwner = "Стол";
            IInventoryPresenter table = consoleInventoryFactory.CreateInventory(inventoryOwner);

            Item book = new Item("Книга");
            Item coin = new Item("Монета");
            Item fetish = new Item("Амулет");

            table.AddItem(book, 1);
            table.AddItem(coin, 33);
            table.AddItem(fetish, 1);

            inventoryOwner = "Игрок";
            IInventoryPresenter player = consoleInventoryFactory.CreateInventory(inventoryOwner);

            Console.Clear();

            TakeAllItems(table, player);

            table.ShowItems();
            player.ShowItems();
        }

        private static void TakeAllItems(IInventoryPresenter inventoryFrom, IInventoryPresenter inventoryTo)
        {
            const string RepeateMessage = "Ошибка, попробуй еще раз";

            while (inventoryFrom.IsEmpty == false)
            {
                inventoryFrom.ShowItems();
                inventoryTo.ShowItems();

                if (TryTakeItemId(inventoryFrom, out uint id))
                {
                    string message = "Сколько берем?";

                    if (TryGetUserUintInput(message, out uint count))
                    {
                        if (inventoryFrom.TryGetItems(id, count, out IItem item))
                        {
                            inventoryTo.AddItem(item, count);
                        }
                        else
                        {
                            Console.WriteLine("Нет такого колличества " + RepeateMessage);
                        }
                    }
                    else
                    {
                        Console.WriteLine(RepeateMessage);
                    }
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private static bool TryTakeItemId(IInventoryPresenter inventory, out uint id)
        {
            bool isSuccess = false;

            string message = "Выберете товар по номеру";

            if (TryGetUserUintInput(message, out id))
            {
                uint itemCount = 0;

                if (inventory.TryGetItems(id, itemCount, out IItem item))
                {
                    isSuccess = true;
                }
            }

            return isSuccess;
        }

        private static bool TryGetUserUintInput(String message, out uint userUint)
        {
            bool isSuccess = false;

            Console.WriteLine(message);

            if (uint.TryParse(Console.ReadLine(), out userUint))
            {
                isSuccess = true;
            }
            else
            {
                Console.WriteLine("Введенное не является целым положительным числом");
            }

            return isSuccess;
        }
    }
}