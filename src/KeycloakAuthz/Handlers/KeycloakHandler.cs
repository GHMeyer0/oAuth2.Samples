using Jose;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KeycloakAuthz.Handlers
{
    public class KeycloakHandler : AuthorizationHandler<IAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public KeycloakHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
        {
            var resource = context.Resource as RouteEndpoint;

            System.Console.WriteLine(resource);
            var httpContext = httpContextAccessor.HttpContext;
            string authHeader = httpContext.Request.Headers["Authorization"];

            var tokenHandler = new JwtSecurityTokenHandler();

            var empresas = new Informacoes
            {
               
            };

            //var informacoesExtras = ObjectToString(infos);

            var empresa = new List<string> { "34229055000190" };
            var infos = new
            {
                organization = empresa
            };

            var json = JsonConvert.SerializeObject(infos);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));




            string url = "https://tuneauth.com.br/auth/realms/tiino-dev/protocol/openid-connect/token";
            
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);


            request.AddHeader("Authorization", authHeader);
            request.AddParameter("grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket");
            request.AddParameter("audience", "tiino.bff.pedidos");
            request.AddParameter("claim_token", base64);
            request.AddParameter("claim_token_format", "urn:ietf:params:oauth:token-type:jwt");
            request.AddParameter("response_include_resource_name", "true");
            request.AddParameter("permission", "Pedidos");
            request.AddParameter("response_mode", "decision");

            while (true)
            {
                var response = client.Execute(request);
            }
            
            //var result = JsonConvert.DeserializeObject<Response>(response.Content);

            //if (result.result)
            //{
            //    context.Succeed(requirement);
            //}
            //else
            //{
            //    context.Fail();
            //}
            

            return Task.CompletedTask;
        }
        private string ObjectToString(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
    [Serializable]
    internal class Informacoes
    {
        public List<string> empresa { get; set; }
    }
    [Serializable]
    internal class Response
    {
        public bool result { get; set; }
    }

}


