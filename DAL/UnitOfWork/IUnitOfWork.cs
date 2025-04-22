using DAL.Repositories;
using Domain.Entities;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Place> Places { get; }
        IRepository<Review> Reviews { get; }
        IRepository<Question> Questions { get; }
        IRepository<Visit> Visits { get; }
        IRepository<MediaFile> MediaFiles { get; }

        Task<int> SaveAsync();
    }
}
