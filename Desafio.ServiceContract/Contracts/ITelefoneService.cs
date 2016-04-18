using Desafio.ServiceContract.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.ServiceContract.Contracts
{
    public interface ITelefoneService
    {
        TelefoneViewModel SaveTelefone(TelefoneViewModel telefoneToSave);

        void RemoveTelefone(int telefoneId);

        IList<TelefoneViewModel> Find(string telefone = "");

        TelefoneViewModel GetById(int telefoneId);

        void ServiceInitialize();
    }
}
