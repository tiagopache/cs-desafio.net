using Desafio.Application.Contract.Contracts.Base;
using Desafio.Application.Contract.ViewModels;
using System.Collections.Generic;

namespace Desafio.Application.Contract.Contracts
{
    public interface IUsuarioApplicationService : IApplicationServiceBase<UsuarioViewModel>
    {
        UsuarioViewModel Signup(UsuarioViewModel usuarioToSave);

        IList<UsuarioViewModel> Find(string nomeouemail = "");

        UsuarioViewModel Login(LoginViewModel login);
    }
}
