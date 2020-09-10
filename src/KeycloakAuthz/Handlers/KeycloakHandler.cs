using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using RestSharp;
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


            string url = "https://tuneauth.com.br/auth/realms/excelencia-dev/protocol/openid-connect/token";
            
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);


            request.AddHeader("Authorization", authHeader);
            request.AddParameter("grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket");
            request.AddParameter("audience", "exemple-api");
            request.AddParameter("response_include_resource_name", "true");
            request.AddParameter("permission", resource.RoutePattern.Defaults["controller"]);
            request.AddParameter("response_mode", "decision");

            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<Response>(response.Content);

            if (result.result)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            

            return Task.CompletedTask;
        }
    }
}
internal class Response
{
    public bool result { get; set; }
}
