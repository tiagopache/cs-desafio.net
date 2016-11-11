using Desafio.Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Desafio.Business.Contract.Base
{
    public interface IBusinessServiceBase<TEntity> where TEntity : BaseEntity
    {
        TEntity GetById(int id);

        IList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderedBy = null, string includeProperties = null);

        TEntity Save(TEntity toSave);

        void Remove(int id);

        void Remove(TEntity toRemove);
    }
}
