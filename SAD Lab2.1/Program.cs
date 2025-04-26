using SAD_Lab2._1;
using DAL.Data;

namespace SAD_Lab2._1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var dbContext = new AppDbContext();
            var menu = new ConsoleMenu(dbContext);
            await menu.RunAsync();
        }
    }
}