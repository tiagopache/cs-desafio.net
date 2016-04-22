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
        public IHttpActionResult Signup([FromBody] SignupRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = new UsuarioViewModel()
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    Senha = model.Senha
                };

                foreach (var tel in model.Telefones)
                {
                    user.Telefones.Add(tel);
                }

                var result = this.UsuarioService.Signup(user);

                return Created<UsuarioViewModel>("Profile", result);
            }
            catch (Exception ex)
            {
                return Content<ErrorViewModel>(HttpStatusCode.BadRequest, new ErrorViewModel() { Mensagem = ex.Message, StatusCode = (int)HttpStatusCode.BadRequest });
            }
        }

        [HttpPost]
        [Route("api/login")]
        [ResponseType(typeof(UsuarioViewModel))]
        public IHttpActionResult Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content<ErrorViewModel>(HttpStatusCode.BadRequest, new ErrorViewModel() { Mensagem = "Dados Inválidos", StatusCode = (int)HttpStatusCode.BadRequest });
            }

            try
            {
                var result = this.UsuarioService.Login(model);

                return Ok<UsuarioViewModel>(result);
            }
            catch (Exception ex)
            {
                return Content<ErrorViewModel>(HttpStatusCode.BadRequest, new ErrorViewModel() { Mensagem = ex.Message, StatusCode = (int)HttpStatusCode.BadRequest });
            }
        }

        [HttpGet]
        [Route("api/profile/{id}")]
        [ResponseType(typeof(UsuarioViewModel))]
        [Authorize]
        public IHttpActionResult Profile(string id)
        {
            try
            {
                var usuario = this.UsuarioService.GetById(int.Parse(id));

                return Ok<UsuarioViewModel>(usuario);
            }
            catch (Exception ex)
            {
                return Content<ErrorViewModel>(HttpStatusCode.BadRequest, new ErrorViewModel() { Mensagem = ex.Message, StatusCode = (int)HttpStatusCode.BadRequest });
            }
        }
    }
}
