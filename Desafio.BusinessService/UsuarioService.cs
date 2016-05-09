using Desafio.Model;
using Desafio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desafio.Infrastructure.Security;

namespace Desafio.BusinessService
{
    public class UsuarioService : ServiceBase
    {
        public UsuarioService() : base()
        {

        }

        public UsuarioService(DesafioDbContext context) : base(context)
        {

        }

        public bool ValidatePassword(Usuario usuario, string senha)
        {
            var exMessage = "Usuário e/ou senha inválidos.";

            if (usuario == null)
                throw new UnauthorizedAccessException(exMessage);

            return Desafio.Infrastructure.Security.Cryptography.VerifyHashedPassword(usuario.Senha, senha);
        }

        public Usuario SaveUsuario(Usuario usuarioToSave, bool generateNewToken = false)
        {
            var usuarioFound = this.UnitOfWork.UsuarioRepository.GetById(usuarioToSave.Id) ?? this.FindByEmail(usuarioToSave.Email);

            if (usuarioFound != null)
            {
                usuarioFound.Nome = usuarioToSave.Nome;
                usuarioFound.Email = usuarioToSave.Email;
                usuarioFound.DataAtualizacao = DateTime.Now;
                usuarioFound.UltimoLogin = usuarioToSave.UltimoLogin;
                if (generateNewToken)
                    usuarioFound.Token = Cryptography.CreateJwt(usuarioFound.Id, usuarioFound.Email);

                this.UnitOfWork.UsuarioRepository.Update(usuarioFound);
            }
            else
            {
                usuarioToSave.DataCriacao = DateTime.Now;
                usuarioToSave.DataAtualizacao = DateTime.Now;
                usuarioToSave.UltimoLogin = usuarioToSave.DataAtualizacao;
                usuarioToSave.Senha = usuarioToSave.Senha.GetPasswordHash();

                this.UnitOfWork.UsuarioRepository.Insert(usuarioToSave);
            }

            this.UnitOfWork.Save();

            return usuarioToSave;
        }

        public void RemoveUsuario(int usuarioId)
        {
            this.UnitOfWork.UsuarioRepository.Delete(usuarioId);

            this.UnitOfWork.Save();
        }

        public IList<Usuario> Find(string nomeouemail = "")
        {
            var result = default(List<Usuario>);

            if (!string.IsNullOrWhiteSpace(nomeouemail))
                result = this.UnitOfWork.UsuarioRepository.Get(u => u.Nome.Contains(nomeouemail) || u.Email.Contains(nomeouemail)).ToList();
            else
                result = this.UnitOfWork.UsuarioRepository.Get().ToList();

            return result;
        }

        public Usuario FindByEmail(string email)
        {
            var result = default(Usuario);

            if (!string.IsNullOrWhiteSpace(email))
                result = this.UnitOfWork.UsuarioRepository.Get(u => u.Email.Equals(email)).FirstOrDefault();
            else
                result = null;

            return result;
        }

        public Usuario GetById(int usuarioId)
        {
            return this.UnitOfWork.UsuarioRepository.GetById(usuarioId);
        }

        public Usuario CreateJwtToken(int id)
        {
            var usuario = this.GetById(id);

            usuario.Token = Cryptography.CreateJwt(usuario.Id, usuario.Email);

            this.UnitOfWork.UsuarioRepository.Update(usuario);

            this.UnitOfWork.Save();

            return usuario;
        }

    }
}
