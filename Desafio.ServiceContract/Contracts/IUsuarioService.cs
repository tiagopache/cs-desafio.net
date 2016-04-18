using Desafio.ServiceContract.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.ServiceContract.Contracts
{
    public interface IUsuarioService
    {
        UsuarioViewModel SaveUsuario(UsuarioViewModel usuarioToSave);

        void RemoveUsuario(int usuarioId);

        IList<UsuarioViewModel> Find(string nomeouemail = "");

        UsuarioViewModel GetById(int usuarioId);

        UsuarioViewModel Login(LoginViewModel login);

        void ServiceInitialize();
    }
}
