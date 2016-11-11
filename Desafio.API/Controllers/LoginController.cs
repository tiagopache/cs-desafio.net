using Desafio.Application.Contract.Contracts;
using Desafio.Application.Contract.ViewModels;
using Desafio.Infrastructure.Extensions;
using Desafio.Infrastructure.Security;
using Microsoft.Practices.Unity;
using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Desafio.API.Controllers
{
    public class LoginController : ApiController
    {
        private IUsuarioApplicationService _usuarioApplicationService { get; set; }

        public LoginController() { }

        [InjectionConstructor]
        public LoginController(IUsuarioApplicationService usuarioApplicationService)
        {
            this._usuarioApplicationService = usuarioApplicationService;
        }

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

                var result = this._usuarioApplicationService.Signup(user);

                return Created<UsuarioViewModel>("Profile", result);
            }
            catch (Exception ex)
            {
                return Content<ErrorViewModel>(HttpStatusCode.BadRequest, new ErrorViewModel() { Mensagem = ex.GetFullMessage(), StatusCode = (int)HttpStatusCode.BadRequest });
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
                var result = this._usuarioApplicationService.Login(model);

                return Ok<UsuarioViewModel>(result);
            }
            catch (Exception ex)
            {
                return Content<ErrorViewModel>(HttpStatusCode.BadRequest, new ErrorViewModel() { Mensagem = ex.GetFullMessage(), StatusCode = (int)HttpStatusCode.BadRequest });
            }
        }

        [HttpGet]
        [Route("api/profile/{id}")]
        [ResponseType(typeof(UsuarioViewModel))]
        public IHttpActionResult Profile(int id)
        {
            try
            {
                int sub = int.Parse(validateAuthorization());

                if (sub == id)
                {
                    var usuario = this._usuarioApplicationService.GetById(sub);

                    return Ok<UsuarioViewModel>(usuario);
                }
                else
                    throw new UnauthorizedAccessException("Não autorizado.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Content<ErrorViewModel>(HttpStatusCode.Unauthorized, new ErrorViewModel() { Mensagem = ex.GetFullMessage(), StatusCode = (int)HttpStatusCode.Unauthorized });
            }
            catch (Exception ex)
            {
                return Content<ErrorViewModel>(HttpStatusCode.BadRequest, new ErrorViewModel() { Mensagem = ex.GetFullMessage(), StatusCode = (int)HttpStatusCode.BadRequest });
            }
        }

        private string validateAuthorization()
        {
            var authorization = this.Request.Headers.Authorization.Parameter;

            var claims = Cryptography.ValidateJwt(authorization);

            var sub = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value;

            return sub;
        }
    }
}
