using Desafio.API.Models;
using Desafio.ApplicationService;
using Desafio.ServiceContract.Contracts;
using Desafio.ServiceContract.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Desafio.API.Controllers
{
    public class LoginController : ApiController
    {

        #region Singleton Pattern - UsuarioService
        private IUsuarioService _usuarioService;
        public IUsuarioService UsuarioService
        {
            get
            {
                if (_usuarioService == null)
                    _usuarioService = new UsuarioService();

                return _usuarioService;
            }
            set
            {
                _usuarioService = value;
            }
        }
        #endregion

        [HttpPost]
        [Route("api/signup")]
        [ResponseType(typeof(UsuarioViewModel))]
        public IHttpActionResult Signup([FromBody] UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = this.UsuarioService.SaveUsuario(model);

            return CreatedAtRoute("DefaultApi", new { id = result.Id }, result);
        }

        [HttpPost]
        [Route("api/login")]
        [ResponseType(typeof(UsuarioViewModel))]
        public IHttpActionResult Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                HttpError erro = new HttpError("Dados Inválidos") { { "StatusCode", (int)HttpStatusCode.BadRequest } };
                //return new HttpResponseException(erro);
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, erro);
            }

            var result = this.UsuarioService.Login(model);

            return Ok<UsuarioViewModel>(result);
        }
    }
}
