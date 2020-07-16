using System;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using RestSharp;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var token = GetToken();

            var url = "https://localhost:6001/WeatherForecast";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + token.access_token, ParameterType.HttpHeader);

            //make the API request and get a response
            IRestResponse response = client.Execute(request);

        }

        private static TokenValidationResponse GetToken()
        {
            string url = "http://192.168.0.106:8080/auth/realms/excelencia-cobrancas/protocol/openid-connect/token";
            var client = new RestClient(url);
            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("dotnet-client", "139bc0a9-fe74-4083-b18f-e0b4fe956777");
            var request = new RestRequest(Method.POST);

            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", "dotnet-client");
            request.AddParameter("client_secret", "139bc0a9-fe74-4083-b18f-e0b4fe956777");
            request.AddParameter("scope", "email");
            var response = client.Post(request);
            return JsonConvert.DeserializeObject<TokenValidationResponse>(response.Content);
        }
    }

    internal class TokenValidationResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }

        public string scope { get; set; }
    }
}
