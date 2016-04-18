using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desafio.Infrastructure.Extensions;

namespace Desafio.ServiceContract.ViewModels
{
    public abstract class BaseViewModel<TContract, TEntity> where TContract: class
                                                            where TEntity: class
    {
        public static TContract ToContract(TEntity entity)
        {
            if (entity == null)
                return null;

            var result = default(TContract);

            result = entity.Copy<TContract>();

            return result;
        }

        public static IEnumerable<TContract> ToContract(IEnumerable<TEntity> entity)
        {
            if (entity == null)
                return null;

            var result = new List<TContract>();

            foreach (var ent in entity)
            {
                result.Add(BaseViewModel<TContract, TEntity>.ToContract(ent));
            }

            return result;
        }

        public static TEntity ToEntity(TContract viewModel)
        {
            if (viewModel == null)
                return null;

            var result = viewModel.Copy<TEntity>();

            return result;
        }
    }
}
