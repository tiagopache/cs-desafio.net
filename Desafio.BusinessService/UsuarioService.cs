using Desafio.Model;
using Desafio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Usuario SaveUsuario(Usuario usuarioToSave)
        {
            var usuarioFound = this.UnitOfWork.UsuarioRepository.GetById(usuarioToSave.Id);

            if (usuarioFound != null)
            {
                usuarioFound.Nome = usuarioToSave.Nome;
                usuarioFound.Email = usuarioToSave.Email;
                usuarioFound.Senha = usuarioToSave.Senha;
                usuarioFound.DataCriacao = usuarioToSave.DataCriacao;
                usuarioFound.DataAtualizacao = DateTime.Now;
                usuarioToSave.Token = usuarioToSave.Token;

                this.UnitOfWork.UsuarioRepository.Update(usuarioFound);
            }
            else
            {
                usuarioToSave.DataCriacao = DateTime.Now;
                usuarioToSave.DataAtualizacao = usuarioToSave.DataCriacao;
                usuarioToSave.UltimoLogin = usuarioToSave.DataCriacao;
                usuarioToSave.Token = "";

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

    }
}
