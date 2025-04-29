using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    // Контекст бази даних, що забезпечує доступ до таблиць Users, Places, Reviews, Questions, Visits та MediaFiles за допомогою Entity Framework.
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<MediaFile> MediaFiles { get; set; }

        // Автоматичне формування шляху до БД
        public string DatabasePath { get; }

        public AppDbContext()
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            DatabasePath = Path.Combine(folder, "places.db");
        }

        // Налаштування підключення до SQLite з використанням лінивого завантаження та вказівкою шляху до БД.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()  // Ліниве завантаження активоване
                .UseSqlite($"Data Source={DatabasePath}");
        }

        // Налаштування моделей при їх формуванні.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}