using Desafio.ServiceContract.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desafio.ServiceContract.ViewModels;
using Desafio.Infrastructure.Security;

namespace Desafio.ApplicationService
{
    public class UsuarioService : IUsuarioService
    {

        #region Singleton Pattern - DomainUsuarioService
        private Desafio.BusinessService.UsuarioService _domainUsuarioService;
        public Desafio.BusinessService.UsuarioService DomainUsuarioService
        {
            get
            {
                if (_domainUsuarioService == null)
                    _domainUsuarioService = new Desafio.BusinessService.UsuarioService();

                return _domainUsuarioService;
            }
            set
            {
                _domainUsuarioService = value;
            }
        }
        #endregion
        
        #region Singleton Pattern - DomainTelefoneService
        private Desafio.BusinessService.TelefoneService _domainTelefoneService;
        public Desafio.BusinessService.TelefoneService DomainTelefoneService
        {
            get
            {
                if (_domainTelefoneService == null)
                    _domainTelefoneService = new Desafio.BusinessService.TelefoneService();

                return _domainTelefoneService;
            }
            set
            {
                _domainTelefoneService = value;
            }
        }
        #endregion


        public IList<UsuarioViewModel> Find(string nomeouemail = "")
        {
            return UsuarioViewModel.ToContract(this.DomainUsuarioService.Find(nomeouemail)).ToList();
        }

        public UsuarioViewModel GetById(int usuarioId)
        {
            return UsuarioViewModel.ToContract(this.DomainUsuarioService.GetById(usuarioId));
        }

        public UsuarioViewModel Login(LoginViewModel login)
        {
            var usuario = UsuarioViewModel.ToContract(this.DomainUsuarioService.FindByEmail(login.Email));

            var exMessage = "Usuário e/ou senha inválidos.";

            if (usuario != null)
            {
                if (usuario.Senha.GetSHA512Hash() != login.Senha.GetSHA512Hash())
                {
                    throw new UnauthorizedAccessException(exMessage);
                }
                else
                {
                    usuario.UltimoLogin = DateTime.Now;

                    usuario = this.SaveUsuario(usuario);
                }
            }
            else
            {
                throw new UnauthorizedAccessException(exMessage);
            }

            return usuario;
        }

        public void RemoveUsuario(int usuarioId)
        {
            this.DomainUsuarioService.RemoveUsuario(usuarioId);
        }

        public UsuarioViewModel SaveUsuario(UsuarioViewModel usuarioToSave)
        {


            return UsuarioViewModel.ToContract(this.DomainUsuarioService.SaveUsuario(UsuarioViewModel.ToEntity(usuarioToSave)));
        }

        public void ServiceInitialize()
        {
            this.DomainUsuarioService.ServiceInitialize();
        }



    }
}
