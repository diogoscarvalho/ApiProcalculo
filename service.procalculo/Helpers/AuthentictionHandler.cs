using BusinessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace service.procalculo.Helpers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string username = null;
            if (IsValid(request, out username))
            {
                var principal = new GenericPrincipal(new GenericIdentity(username), null); Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null) HttpContext.Current.User = principal;
                return base.SendAsync(request, cancellationToken);
            }
            else
            {
                return Task.Factory.StartNew(() =>
                {
                    var r = new HttpResponseMessage(HttpStatusCode.Unauthorized); r.Headers.Add("WWW-Authenticate", "Basic realm=\"Service-Mactada\""); 
                    return r;
                });
            }
        }

        private static bool IsValid(HttpRequestMessage request, out string username)
        {
            username = null; var header = request.Headers.Authorization;
            if (header != null && header.Scheme == "Basic")
            {
                var credentials = header.Parameter;
                if (!string.IsNullOrWhiteSpace(credentials))
                {
                    var decodedCredentials = Encoding.Default.GetString(Convert.FromBase64String(credentials));
                    var separator = decodedCredentials.IndexOf(':'); 
                    var password = decodedCredentials.Substring(separator + 1);
                    username = decodedCredentials.Substring(0, separator);
                    
                    // Verifica se existe o usuário com as informações enviadas.
                    Model.Usuario usuario =  new UsuarioBusiness().SelecionarUsuario(username, password).Result;

                    return usuario != null;
                }
            }
            return false;
        }
    }
}