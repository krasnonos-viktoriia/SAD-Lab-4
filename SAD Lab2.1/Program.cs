using AutoMapper;
using BLL.MappingProfiles;
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

            var config = new MapperConfiguration(cfg =>
            {
                // Тут додайте свої профілі мапінгу
                cfg.AddProfile<MappingProfile>(); // приклад
            });

            IMapper mapper = config.CreateMapper();

            // Ініціалізація контексту бази даних і запуск меню.
            var dbContext = new AppDbContext();
            var menu = new ConsoleMenu(dbContext, mapper);
            await menu.RunAsync();
        }
    }
}