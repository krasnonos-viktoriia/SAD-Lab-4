using SAD_Lab2._1;
using DAL.Data;
using System.Text;

namespace SAD_Lab2._1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Console.InputEncoding = Encoding.GetEncoding(1251); 
            Console.OutputEncoding = Encoding.UTF8;

            var dbContext = new AppDbContext();
            var menu = new ConsoleMenu(dbContext);
            await menu.RunAsync();
        }
    }
}