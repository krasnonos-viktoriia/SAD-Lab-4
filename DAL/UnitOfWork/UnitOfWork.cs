using DAL.Repositories;
using Domain.Entities;
using SAD_Lab2._1.DAL.Data;

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
            Users = new GenericRepository<User>(context);
            Places = new GenericRepository<Place>(context);
            Reviews = new GenericRepository<Review>(context);
            Questions = new GenericRepository<Question>(context);
            Visits = new GenericRepository<Visit>(context);
            MediaFiles = new GenericRepository<MediaFile>(context);
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
