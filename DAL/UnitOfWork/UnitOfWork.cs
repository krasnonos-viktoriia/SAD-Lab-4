using DAL.Repositories;
using Domain.Entities;
using DAL.Data;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<User> Users { get; }
        public IRepository<Place> Places { get; }
        public IRepository<Review> Reviews { get; }
        public IRepository<Question> Questions { get; }
        public IRepository<Visit> Visits { get; }
        public IRepository<MediaFile> MediaFiles { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new Repository<User>(context);
            Places = new Repository<Place>(context);
            Reviews = new Repository<Review>(context);
            Questions = new Repository<Question>(context);
            Visits = new Repository<Visit>(context);
            MediaFiles = new Repository<MediaFile>(context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
