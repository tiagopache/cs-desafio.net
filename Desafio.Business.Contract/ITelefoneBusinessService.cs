using Desafio.Business.Contract.Base;
using Desafio.Model.Entities;
using System.Collections.Generic;

namespace Desafio.Business.Contract
{
    public interface ITelefoneBusinessService : IBusinessServiceBase<Telefone>
    {
        IList<Telefone> Find(string telefone = "");
    }
}
