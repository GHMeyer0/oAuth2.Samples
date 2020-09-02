using Microsoft.AspNetCore.Authorization;
using RestSharp;
using System.Threading.Tasks;

namespace KeycloakAuthz.Handlers
{
    public class KeycloakHandler : AuthorizationHandler<IAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
        {
            var authFilterCtx = (Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext)context.Resource;
            string authHeader = authFilterCtx.HttpContext.Request.Headers["Authorization"];

            string url = "https://tuneauth.com.br/auth/realms/excelencia-dev/protocol/openid-connect/token";
            
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);


            request.AddHeader("Authorization", authHeader);
            request.AddParameter("grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket");
            request.AddParameter("audience", "exemple-api");
            request.AddParameter("response_include_resource_name", "true");
            request.AddParameter("permission", context.Resource);
            request.AddParameter("response_mode", "decision");

            var aa = client.Execute(request);
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
