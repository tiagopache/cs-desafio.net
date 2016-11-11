using Desafio.Infrastructure.Data;
using Desafio.Infrastructure.Data.Base;

namespace Desafio.BusinessService.Base
{
    public abstract class ServiceIdBase<TEntity> : ServiceBase<TEntity> where TEntity : BaseIdEntity
    {
        public ServiceIdBase(IUnitOfWork<TEntity> uow) : base(uow)
        {

        }

        public virtual TEntity GetById(int id)
        {
            return this.unitOfWork.Repository.GetById(id);
        }

        public virtual void Remove(int id)
        {
            this.unitOfWork.Repository.Delete(id);

            this.unitOfWork.Save();
        }

        public virtual TEntity Save(TEntity toSave)
        {
            var found = this.unitOfWork.Repository.GetById(toSave.Id);

            return this.save(toSave, found);
        }
    }
}
