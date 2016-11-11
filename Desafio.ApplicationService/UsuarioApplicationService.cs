using Desafio.Application.Contract.Contracts;
using Desafio.Application.Contract.ViewModels;
using Desafio.Business.Contract;
using Desafio.Infrastructure.Security;
using Desafio.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio.Application.Service
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        private IUsuarioBusinessService _usuarioBusinessService { get; set; }

        private ITelefoneBusinessService _telefoneBusinessService { get; set; }

        public UsuarioApplicationService(IUsuarioBusinessService usuarioBusinessService, ITelefoneBusinessService telefoneBusinessService)
        {
            this._usuarioBusinessService = usuarioBusinessService;
            this._telefoneBusinessService = telefoneBusinessService;
        }

        public IList<UsuarioViewModel> Find(string nomeouemail = "")
        {
            return UsuarioViewModel.Instance.ToContract(this._usuarioBusinessService.Find(nomeouemail)).ToList();
        }

        public UsuarioViewModel Login(LoginViewModel login)
        {
            var usuarioModel = this._usuarioBusinessService.FindByEmail(login.Email);
            UsuarioViewModel result = null;

            var exMessage = "Usuário e/ou senha inválidos.";

            if (usuarioModel != null)
            {
                if (!this._usuarioBusinessService.ValidatePassword(usuarioModel, login.Senha))
                {
                    throw new UnauthorizedAccessException(exMessage);
                }
                else
                {
                    try
                    {
                        Cryptography.ValidateJwt(usuarioModel.Token);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        usuarioModel = this._usuarioBusinessService.CreateJwtToken(usuarioModel.Id);
                    }

                    usuarioModel.UltimoLogin = DateTime.Now;

                    usuarioModel = this._usuarioBusinessService.Save(usuarioModel, false);

                    result = this.RetrieveUsuarioWithTelefones(usuarioModel);
                }
            }
            else
            {
                throw new UnauthorizedAccessException(exMessage);
            }

            return result;
        }
        public UsuarioViewModel Signup(UsuarioViewModel usuarioToSave)
        {
            if (this._usuarioBusinessService.FindByEmail(usuarioToSave.Email) != null)
                throw new Exception("E-mail já existente.");

            var usuarioSaved = this._usuarioBusinessService.Save(UsuarioViewModel.Instance.ToEntity(usuarioToSave), false);

            foreach (var tel in usuarioToSave.Telefones)
            {
                var telToSave = TelefoneViewModel.Instance.ToEntity(tel);
                telToSave.IdUsuario = usuarioSaved.Id;

                this._telefoneBusinessService.Save(telToSave);
            }

            usuarioSaved = this._usuarioBusinessService.CreateJwtToken(usuarioSaved.Id);

            // Must get a new context to view reflection of other dbcontext entity changes
            usuarioSaved = this._usuarioBusinessService.FindByEmail(usuarioToSave.Email);

            UsuarioViewModel result = RetrieveUsuarioWithTelefones(usuarioSaved);

            return result;
        }

        private UsuarioViewModel RetrieveUsuarioWithTelefones(Usuario usuarioSaved)
        {
            var result = UsuarioViewModel.Instance.ToContract(usuarioSaved);

            foreach (var tel in usuarioSaved.Telefones)
            {
                result.Telefones.Add(TelefoneViewModel.Instance.ToContract(tel));
            }

            return result;
        }

        public IList<UsuarioViewModel> Get(string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public UsuarioViewModel GetById(object id)
        {
            var usuario = this._usuarioBusinessService.GetById(Convert.ToInt32(id));

            var result = this._usuarioBusinessService.FindByEmail(usuario.Email);

            return this.RetrieveUsuarioWithTelefones(result);
        }

        public UsuarioViewModel Save(UsuarioViewModel contract)
        {
            throw new NotImplementedException();
        }

        public void Remove(object id)
        {
            this._usuarioBusinessService.Remove(Convert.ToInt32(id));
        }
    }
}
