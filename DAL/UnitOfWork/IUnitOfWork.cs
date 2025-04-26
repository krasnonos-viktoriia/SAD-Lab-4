using DAL.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveAsync();
    }
}
