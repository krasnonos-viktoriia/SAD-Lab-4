using DAL.Data;
using System.Text;

namespace SAD_Lab2._1
{
    // Головна програма для запуску додатку, налаштування кодування консолі та ініціалізації меню.
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Реєстрація підтримки кодування для українських та інших символів.
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Налаштування кодування вводу та виводу в консолі.
            Console.InputEncoding = Encoding.GetEncoding(1251); // Встановлення кодування для вводу.
            Console.OutputEncoding = Encoding.UTF8; // Встановлення кодування для виводу.

            // Ініціалізація контексту бази даних і запуск меню.
            var dbContext = new AppDbContext();
            var menu = new ConsoleMenu(dbContext);
            await menu.RunAsync();
        }
    }
}