using Desafio.ServiceContract.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http;
using System.Threading;

namespace Desafio.API.Filters
{
    public class CustomAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //base.OnAuthorization(actionContext);

            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader == null)
            {
                throw new UnauthorizedAccessException();
            }

            var bearer = authHeader.Parameter;
        }
    }

    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);

            //if (actionExecutedContext.Exception is Exception)
            //{
            //    actionExecutedContext.Response = Content<ErrorViewModel>(); //new HttpResponseMessage(HttpStatusCode.InternalServerError, )
            //}
        }
    }


    class OopsExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            //base.Handle(context);
            context.Result = new TextPlainErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = "Oops! Sorry! Something went wrong." +
                          "Please contact support@contoso.com so we can try to fix it."
            };
        }

        private class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public string Content { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response =
                                 new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(Content);
                response.RequestMessage = Request;
                return Task.FromResult(response);
            }
        }
    }
}
