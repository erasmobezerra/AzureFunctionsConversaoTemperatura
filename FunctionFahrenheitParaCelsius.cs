using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ConversaoTemperatura
{
    public class FunctionFahrenheitParaCelsius
    {
        private readonly ILogger<FunctionFahrenheitParaCelsius> _logger;

        public FunctionFahrenheitParaCelsius(ILogger<FunctionFahrenheitParaCelsius> logger)
        {
            _logger = logger;
        }

        [Function("ConversaoTemperatura")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Conversão" })]
        [OpenApiParameter(name: "fahrenheit", In = ParameterLocation.Path, Required = true, Type = typeof(double), Description ="O valor em **fahrenheit** para conversão em Celsius")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Retorna o valor em Celsius")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "conversao-fahrenheit-para-celsius/{fahrenheit}")]
            HttpRequest req, double fahrenheit)
        {
            _logger.LogInformation($"Parâmetro recebido: {fahrenheit}", fahrenheit);

            var valorEmCelsius = (fahrenheit - 32) * 5 / 9;

            string responseMessage = $"O valor em fahrenheit {fahrenheit} em celsius é {valorEmCelsius:F2}";

            _logger.LogInformation($"Conversão efetuada. Resultado: {valorEmCelsius:F2}ºC");

            return new OkObjectResult(responseMessage);
        }
    }
}