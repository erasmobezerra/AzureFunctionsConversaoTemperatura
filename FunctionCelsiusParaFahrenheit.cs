using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ConversaoTemperatura
{
    public class FunctionCelsiusParaFahrenheit
    {
        private readonly ILogger<FunctionCelsiusParaFahrenheit> _logger;

        public FunctionCelsiusParaFahrenheit(ILogger<FunctionCelsiusParaFahrenheit> logger)
        {
            _logger = logger;
        }

        [Function("FunctionCelsiusParaFahrenheit")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Conversão" })]
        [OpenApiParameter(name: "celsius", In = ParameterLocation.Path, Required = true, Type = typeof(double), Description = "O valor em **celsius** para conversão em fahrenheit")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Retorna o valor em fahrenheit")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "conversao-celsius-para-fahrenheit/{celsius}")]
            HttpRequest req, double celsius)
        {
            _logger.LogInformation($"Parâmetro recebido: {celsius}", celsius);

            var valorEmFahrenheit = ((celsius * 9) / 5) + 32;

            string responseMessage = $"O valor em celsius {celsius} em fahrenheit é {valorEmFahrenheit:F2}";

            _logger.LogInformation($"Conversão efetuada. Resultado: {valorEmFahrenheit:F2}ºC");

            return new OkObjectResult(responseMessage);
        }
    }
}