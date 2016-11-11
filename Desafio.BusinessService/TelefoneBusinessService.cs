using Desafio.Business.Contract;
using Desafio.BusinessService.Base;
using Desafio.Infrastructure.Data;
using Desafio.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Desafio.BusinessService
{
    public class TelefoneBusinessService : ServiceIdBase<Telefone>, ITelefoneBusinessService
    {
        public TelefoneBusinessService(IUnitOfWork<Telefone> uow) : base(uow) { }

        public IList<Telefone> Find(string telefone = "")
        {
            var result = default(List<Telefone>);

            if (!string.IsNullOrWhiteSpace(telefone))
                result = this.unitOfWork.Repository.Get(t => t.Numero.Contains(telefone)).ToList();
            else
                result = this.unitOfWork.Repository.Get().ToList();

            return result;
        }
    }
}
