using Desafio.Infrastructure.Data.Base;
using Desafio.Infrastructure.Data.Contexts;
using Desafio.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Data.Repositories
{
    public abstract class Repository : IRepository, IDisposable
    {
        protected static int _instanceSequence = 0;

        private bool disposed = true;

        public Repository()
        {
            _instanceSequence++;

            UnityEventLogger.Log.CreateUnityMessage($"{this.GetType().ToString()} {_instanceSequence}");
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            UnityEventLogger.Log.LogUnityMessage($"Disposing {this.GetType().ToString()} {_instanceSequence}");

            if (!this.disposed)
            {
                if (disposing)
                {
                    // do the dispose of other objects here
                }

                UnityEventLogger.Log.DisposeUnityMessage($"{this.GetType().ToString()} {_instanceSequence}");

                _instanceSequence--;

                this.disposed = true;
            }
        }
    }

    public class Repository<TEntity> : Repository, IRepository<TEntity> where TEntity : BaseEntity
    {
        internal DbContext _context;

        internal DbSet<TEntity> _dbSet;

        public Repository(IDbContext context) : base()
        {
            this._context = context as DbContext;
            this._dbSet = _context.Set<TEntity>();
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            UnityEventLogger.Log.LogUnityMessage(this.GetType().ToString() + " " + _instanceSequence + " - Delete(entityToDelete)");

            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);

            _dbSet.Remove(entityToDelete);
        }

        public virtual void Delete(object id)
        {
            UnityEventLogger.Log.LogUnityMessage(this.GetType().ToString() + " " + _instanceSequence + " - Delete(id)");

            TEntity entityToDelete = _dbSet.Find(id);

            this.Delete(entityToDelete);
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderedBy = null, string includeProperties = null)
        {
            UnityEventLogger.Log.LogUnityMessage(this.GetType().ToString() + " " + _instanceSequence + " - Get");
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderedBy != null)
                return orderedBy(query).ToList();
            else
                return query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            UnityEventLogger.Log.LogUnityMessage(this.GetType().ToString() + " " + _instanceSequence + " - GetById");
            return _dbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            UnityEventLogger.Log.LogUnityMessage(this.GetType().ToString() + " " + _instanceSequence + " - GetByIdAsync");
            return await _dbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            UnityEventLogger.Log.LogUnityMessage(this.GetType().ToString() + " " + _instanceSequence + " - Insert");
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            UnityEventLogger.Log.LogUnityMessage(this.GetType().ToString() + " " + _instanceSequence + " - Update");
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
