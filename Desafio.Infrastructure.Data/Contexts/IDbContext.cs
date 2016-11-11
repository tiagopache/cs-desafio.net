using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Data.Contexts
{
    public interface IDbContext : IDisposable
    {
        void Initialize(IDatabaseInitializer<DbContext> databaseInitializer = null);
        Database Database { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
