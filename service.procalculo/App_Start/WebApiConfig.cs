using Newtonsoft.Json.Serialization;
using service.procalculo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace service.procalculo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "SolicitacaoCalculo", action = "Post", id = RouteParameter.Optional }
            );

            // deixa o formato Json como retorno padrão
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // para um Json indentado
            config.Formatters.JsonFormatter.Indent = true;
            // Resolve o camel case para o caso de as variaveis Json venha minúsculas não causem confusão no Objetos C#
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // Retira os detalhes de erro em caso de exceptions
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;

            GlobalConfiguration.Configuration.Filters.Add(
                new RegularToHttpExceptionFilter()
                );

            // Descomentar quando a regra de autorização definida
            config.MessageHandlers.Add(new AuthenticationHandler()); 
        }
    }
}
