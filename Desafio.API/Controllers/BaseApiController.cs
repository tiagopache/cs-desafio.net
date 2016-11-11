using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Results;

namespace Desafio.API.Controllers
{
    public class BaseApiController : ApiController
    {
        protected override UnauthorizedResult Unauthorized(IEnumerable<AuthenticationHeaderValue> challenges)
        {
            return base.Unauthorized(challenges);
        }

        protected override NotFoundResult NotFound()
        {
            return base.NotFound();

            //return Content(HttpStatusCode.NotFound, new ErrorViewModel() { Mensagem = "Recurso não encontrado", StatusCode = (int)HttpStatusCode.NotFound });
        }
    }
}
