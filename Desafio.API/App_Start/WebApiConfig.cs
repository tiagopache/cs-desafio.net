using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace Desafio.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.EnableCors();

            // Web API configuration and services

            //var corsConfig = new WebApiCorsConfiguration();

            // this adds the CorsMessageHandler to the HttpConfiguration's
            // MessageHandlers collection
            //corsConfig.RegisterGlobal(config);

            //config.Filters.Add(new CustomAuthorizationFilterAttribute());

            //corsConfig.AllowAll();

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.JsonFormatter
                        .SerializerSettings
                        .ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new ErrorHandler.AiExceptionFilterAttribute());
        }
    }
}
