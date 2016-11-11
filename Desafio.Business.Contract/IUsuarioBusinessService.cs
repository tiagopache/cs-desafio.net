using Desafio.Business.Contract.Base;
using Desafio.Model.Entities;
using System.Collections.Generic;

namespace Desafio.Business.Contract
{
    public interface IUsuarioBusinessService : IBusinessServiceBase<Usuario>
    {
        bool ValidatePassword(Usuario usuario, string senha);

        IList<Usuario> Find(string nomeouemail = "");

        Usuario FindByEmail(string email);

        Usuario CreateJwtToken(int id);

        Usuario Save(Usuario usuarioToSave, bool generateNewToken = false);
    }
}
