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

            string inventoryOwner = "Стол";
            InventoryPresenter table = CreateInventory(inventoryOwner);

            Item book = new Item("Книга");
            Item coin = new Item("Монета");
            Item fetish = new Item("Амулет");

            table.AddItem(book, 1);
            table.AddItem(coin, 33);
            table.AddItem(fetish, 1);

            inventoryOwner = "Игрок";
            InventoryPresenter player = CreateInventory(inventoryOwner);

            Console.Clear();

            TakeAllItems(table, player);

            table.ShowItems();
            player.ShowItems();
        }

        private static InventoryPresenter CreateInventory(string ownerName)
        {
            IInventoryModel model = new InventoryModel();
            IInventoryView view = new InventoryConsoleView(ownerName);

            return new InventoryPresenter(model, view);
        }

        private static void TakeAllItems(InventoryPresenter inventoryFrom, InventoryPresenter inventoryTo)
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
                        if (inventoryFrom.TryGetItem(id, count, out IItem item))
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

        private static bool TryTakeItemId(InventoryPresenter inventory, out uint id)
        {
            bool isSuccess = false;

            string message = "Выберете товар по номеру";

            if (TryGetUserUintInput(message, out id))
            {
                uint itemCount = 0;

                if (inventory.TryGetItem(id, itemCount, out IItem item))
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