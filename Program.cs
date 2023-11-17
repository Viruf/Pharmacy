using System;

namespace Pharmacy
{
    internal class Program
    {
        static void Main()
        {
            string reedResult = "";
            Products products = new Products();
            Pharmacies pharmacies = new Pharmacies();
            Storages storages = new Storages();
            Lots lots = new Lots();
            while (reedResult != "Закончить" && reedResult != "14")
            {
                Console.WriteLine("Введите команду (цифрой или словами):");
                Console.WriteLine(" 1. Добавить товар");
                Console.WriteLine(" 2. Удалить товар");
                Console.WriteLine(" 3. Показать товары");
                Console.WriteLine(" 4. Добавить аптеку");
                Console.WriteLine(" 5. Удалить аптеку");
                Console.WriteLine(" 6. Показать аптеку");
                Console.WriteLine(" 7. Добавить склад");
                Console.WriteLine(" 8. Удалить склад");
                Console.WriteLine(" 9. Показать склад");
                Console.WriteLine(" 10. Добавить партию");
                Console.WriteLine(" 11. Удалить партию");
                Console.WriteLine(" 12. Показать партии");
                Console.WriteLine(" 13. Показать партии в аптеке");
                Console.WriteLine(" 14. Закончить");
                reedResult = Console.ReadLine();
                switch (reedResult.Trim())
                {
                    case "Добавить товар":
                    case "1":
                        products.Add();
                        break;
                    case "Удалить товар":
                    case "2":
                        products.Delete();
                        break;
                    case "Показать товары":
                    case "3":
                        products.Show();
                        break;
                    case "Добавить аптеку":
                    case "4":
                        pharmacies.Add();
                        break;
                    case "Удалить аптеку":
                    case "5":
                        pharmacies.Delete();
                        break;
                    case "Показать аптеки":
                    case "6":
                        pharmacies.Show();
                        break;
                    case "Добавить склад":
                    case "7":
                        storages.Add();
                        break;
                    case "Удалить склад":
                    case "8":
                        storages.Delete();
                        break;
                    case "Показать склады":
                    case "9":
                        storages.Show();
                        break;
                    case "Добавить партию":
                    case "10":
                        lots.Add();
                        break;
                    case "Удалить партию":
                    case "11":
                        lots.Delete();
                        break;
                    case "Показать партии":
                    case "12":
                        lots.Show();
                        break;
                    case "Показать партии в аптеке":
                    case "13":
                        lots.ShowOnStorage();
                        break;
                    case "Закончить":
                    case "14":
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда!");
                        break;
                }
            }
        }
    }
}
