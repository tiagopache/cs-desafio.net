using Desafio.Business.Contract;
using Desafio.BusinessService.Base;
using Desafio.Infrastructure.Data;
using Desafio.Infrastructure.Security;
using Desafio.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio.BusinessService
{
    public class UsuarioBusinessService : ServiceIdBase<Usuario>, IUsuarioBusinessService
    {
        public UsuarioBusinessService(IUnitOfWork<Usuario> uow) : base(uow) { }

        public bool ValidatePassword(Usuario usuario, string senha)
        {
            var exMessage = "Usuário e/ou senha inválidos.";

            if (usuario == null)
                throw new UnauthorizedAccessException(exMessage);

            return Desafio.Infrastructure.Security.Cryptography.VerifyHashedPassword(usuario.Senha, senha);
        }

        public Usuario Save(Usuario usuarioToSave, bool generateNewToken = false)
        {
            var usuarioFound = this.unitOfWork.Repository.GetById(usuarioToSave.Id) ?? this.FindByEmail(usuarioToSave.Email);

            if (usuarioFound != null)
            {
                usuarioFound.Nome = usuarioToSave.Nome;
                usuarioFound.Email = usuarioToSave.Email;
                usuarioFound.UpdatedOn = DateTime.Now;
                usuarioFound.UltimoLogin = usuarioToSave.UltimoLogin;
                if (generateNewToken)
                    usuarioFound.Token = Cryptography.CreateJwt(usuarioFound.Id, usuarioFound.Email);

                this.unitOfWork.Repository.Update(usuarioFound);
            }
            else
            {
                usuarioToSave.CreatedOn = DateTime.Now;
                usuarioToSave.UpdatedOn = DateTime.Now;
                usuarioToSave.UltimoLogin = usuarioToSave.UpdatedOn;
                usuarioToSave.Senha = usuarioToSave.Senha.GetPasswordHash();

                this.unitOfWork.Repository.Insert(usuarioToSave);
            }

            this.unitOfWork.Save();

            return usuarioToSave;
        }

        public IList<Usuario> Find(string nomeouemail = "")
        {
            var result = default(List<Usuario>);

            if (!string.IsNullOrWhiteSpace(nomeouemail))
                result = this.unitOfWork.Repository.Get(u => u.Nome.Contains(nomeouemail) || u.Email.Contains(nomeouemail)).ToList();
            else
                result = this.unitOfWork.Repository.Get().ToList();

            return result;
        }

        public Usuario FindByEmail(string email)
        {
            var result = default(Usuario);

            if (!string.IsNullOrWhiteSpace(email))
                result = this.unitOfWork.Repository.Get(u => u.Email.Contains(email)).FirstOrDefault();
            else
                result = null;

            return result;
        }

        public Usuario CreateJwtToken(int id)
        {
            var usuario = this.GetById(id);

            usuario.Token = Cryptography.CreateJwt(usuario.Id, usuario.Email);

            this.unitOfWork.Repository.Update(usuario);

            this.unitOfWork.Save();

            return usuario;
        }

    }
}
