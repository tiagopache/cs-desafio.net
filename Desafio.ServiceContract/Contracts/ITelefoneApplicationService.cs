using Desafio.Application.Contract.ViewModels;
using System.Collections.Generic;

namespace Desafio.Application.Contract.Contracts
{
    public interface ITelefoneApplicationService
    {
        TelefoneViewModel SaveTelefone(TelefoneViewModel telefoneToSave);

        void RemoveTelefone(int telefoneId);

        IList<TelefoneViewModel> Find(string telefone = "");

        TelefoneViewModel GetById(int telefoneId);
    }
}
