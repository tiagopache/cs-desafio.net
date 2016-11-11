using Desafio.Infrastructure.Extensions;
using System;
using System.Collections.Generic;

namespace Desafio.Application.Contract.ViewModels.Base
{
    public abstract class BaseViewModel { }

    public abstract class BaseViewModel<TContract, TEntity> : BaseViewModel
        where TContract : BaseViewModel<TContract, TEntity>
        where TEntity : class
    {

        #region Singleton Pattern - Instance
        private static TContract _instance;
        public static TContract Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Activator.CreateInstance<TContract>();

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        #endregion

        protected static TContract BaseToContract(TEntity entity)
        {
            if (entity == null)
                return default(TContract);

            var result = default(TContract);

            result = entity.Copy<TContract>();

            return result;
        }

        protected static IEnumerable<TContract> BaseToContract(IEnumerable<TEntity> entity)
        {
            if (entity == null)
                return default(IEnumerable<TContract>);

            var result = new List<TContract>();

            foreach (var ent in entity)
            {
                result.Add(Instance.ToContract(ent));
            }

            return result;
        }

        protected static TEntity BaseToEntity(TContract viewModel)
        {
            if (viewModel == null)
                return default(TEntity);

            var result = viewModel.Copy<TEntity>();

            return result;
        }

        public virtual TContract ToContract(TEntity entity)
        {
            return BaseToContract(entity);
        }

        public virtual IEnumerable<TContract> ToContract(IEnumerable<TEntity> entity)
        {
            return BaseToContract(entity);
        }

        public virtual TEntity ToEntity(TContract viewModel)
        {
            return BaseToEntity(viewModel);
        }
    }
}
