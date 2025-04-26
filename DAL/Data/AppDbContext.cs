using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite($"Data Source={DatabasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
