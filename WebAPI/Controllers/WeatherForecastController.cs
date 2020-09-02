using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            string url = "https://tuneauth.com.br/auth/realms/excelencia-dev/authz/entitlement/exemple-api";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            var req = new RestRequest(Method.POST);
            req.AddHeader("Authorization", HttpContext.Request.Headers["Authorization"]);
            req.AddParameter("grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket");
            req.AddParameter("audience", "exemple-api");
            req.AddParameter("response_include_resource_name", "true");
            
            req.AddParameter("response_mode", "permissions");
            req.AddParameter("response_mode", "decision");

            var aa = client.Execute(req);


            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", HttpContext.Request.Headers["Authorization"]);
            var response = client.Execute(request);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
