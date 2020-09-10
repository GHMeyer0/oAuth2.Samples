using System;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using RestSharp;

namespace ServerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.ReadKey();
                MakeRequest();
            }
            

            

        }
        private static void MakeRequest()
        {
            var token = GetToken();

            var url = "https://localhost:6001/api/values";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + token.access_token, ParameterType.HttpHeader);

            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);            
        }

        private static TokenValidationResponse GetToken()
        {
            string url = "https://tuneauth.com.br/auth/realms/excelencia-dev/protocol/openid-connect/token";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "a  pplication/json");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", "example-server");
            request.AddParameter("client_secret", "550f0e55-dec8-40b0-9148-0dd501c60c48");
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
