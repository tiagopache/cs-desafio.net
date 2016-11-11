using Desafio.Infrastructure.Data.Base;
using Desafio.Infrastructure.Data.Contexts;
using Desafio.Infrastructure.Data.Repositories;
using Desafio.Infrastructure.Logging;
using System;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private static int _instanceSequence = 0;

        //[Dependency]
        internal IDbContext _context { get; set; }

        public UnitOfWork(IDbContext context)
        {
            _instanceSequence++;

            UnityEventLogger.Log.CreateUnityMessage($"{this.GetType().ToString()} {_instanceSequence}");

            this._context = context;
        }

        public void Save()
        {
            UnityEventLogger.Log.LogUnityMessage($"{this.GetType().ToString()} {_instanceSequence} - Save");

            _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            UnityEventLogger.Log.LogUnityMessage($"{this.GetType().ToString()} {_instanceSequence} - SaveAsync");

            return await _context.SaveChangesAsync();
        }

        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            UnityEventLogger.Log.LogUnityMessage($"Disposing {this.GetType().ToString()} {_instanceSequence}");

            if (!_disposedValue)
            {
                if (disposing)
                    _context.Dispose();

                UnityEventLogger.Log.DisposeUnityMessage($"{this.GetType().ToString()} {_instanceSequence}");

                _instanceSequence--;

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class UnitOfWork<TEntity> : UnitOfWork, IUnitOfWork<TEntity> where TEntity : BaseEntity
    {
        public UnitOfWork(IDbContext context, IRepository<TEntity> repository) : base(context)
        {
            this.Repository = repository;
        }

        //[Dependency]
        public IRepository<TEntity> Repository { get; set; }
    }
}
