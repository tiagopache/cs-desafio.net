using Desafio.ServiceContract.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desafio.ServiceContract.ViewModels;
using Desafio.Infrastructure.Security;
using Desafio.Infrastructure.Extensions;
using Desafio.Model;

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
            var usuario = this.DomainUsuarioService.GetById(usuarioId);

            var result = this.DomainUsuarioService.FindByEmail(usuario.Email);

            return this.RetrieveUsuarioWithTelefones(result);
        }

        public UsuarioViewModel Login(LoginViewModel login)
        {
            var usuarioModel = this.DomainUsuarioService.FindByEmail(login.Email);
            UsuarioViewModel result = null;

            var exMessage = "Usuário e/ou senha inválidos.";

            if (usuarioModel != null)
            {
                if (!this.DomainUsuarioService.ValidatePassword(usuarioModel, login.Senha))
                {
                    throw new UnauthorizedAccessException(exMessage);
                }
                else
                {
                    usuarioModel.UltimoLogin = DateTime.Now;

                    usuarioModel = this.DomainUsuarioService.SaveUsuario(usuarioModel);

                    result = this.RetrieveUsuarioWithTelefones(usuarioModel);
                }
            }
            else
            {
                throw new UnauthorizedAccessException(exMessage);
            }

            return result;
        }

        public void RemoveUsuario(int usuarioId)
        {
            this.DomainUsuarioService.RemoveUsuario(usuarioId);
        }

        public UsuarioViewModel Signup(UsuarioViewModel usuarioToSave)
        {
            if (this.DomainUsuarioService.FindByEmail(usuarioToSave.Email) != null)
                throw new Exception("E-mail já existente.");

            var usuarioSaved = this.DomainUsuarioService.SaveUsuario(UsuarioViewModel.ToEntity(usuarioToSave));

            foreach (var tel in usuarioToSave.Telefones)
            {
                var telToSave = TelefoneViewModel.ToEntity(tel);
                telToSave.IdUsuario = usuarioSaved.Id;

                this.DomainTelefoneService.SaveTelefone(telToSave);
            }
            
            usuarioSaved = this.DomainUsuarioService.CreateJwtToken(usuarioSaved);

            // Must get a new context to view reflection of other dbcontext entity changes
            usuarioSaved = new BusinessService.UsuarioService().FindByEmail(usuarioToSave.Email);

            UsuarioViewModel result = RetrieveUsuarioWithTelefones(usuarioSaved);

            return result;
        }

        private UsuarioViewModel RetrieveUsuarioWithTelefones(Usuario usuarioSaved)
        {
            var result = UsuarioViewModel.ToContract(usuarioSaved);

            foreach (var tel in usuarioSaved.Telefones)
            {
                result.Telefones.Add(TelefoneViewModel.ToContract(tel));
            }

            return result;
        }

        public void ServiceInitialize()
        {
            this.DomainUsuarioService.ServiceInitialize();
        }
    }
}
