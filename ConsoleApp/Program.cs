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
            string url = "https://excelencia.tuneauth.com.br/connect/token";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", "console-app");
            request.AddParameter("scope", "report-api.write");
            request.AddParameter("client_secret", "app-console");

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
