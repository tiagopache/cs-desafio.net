using Desafio.Infrastructure.Data.Base;
using Desafio.Infrastructure.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        Task<int> SaveAsync();
    }

    public interface IUnitOfWork<TEntity> : IUnitOfWork where TEntity : BaseEntity
    {
        IRepository<TEntity> Repository { get; set; }
    }
}
