# 🌡️ Conversão de Temperatura com Azure Functions

Este projeto demonstra como criar uma Azure Function do tipo **HTTP Trigger** com nível de segurança **Anonymous**, utilizando o modelo **in-process** e suporte à documentação via **OpenAPI/Swagger**.

---

## 🚀 Etapas para Configuração

### 1. Criar o Projeto Azure Function

Crie um novo projeto do tipo Azure Function com gatilho HTTP:

```bash
func init ConversaoTemperatura --worker-runtime dotnet
func new --name ConversaoTemperatura --template "HTTP trigger" --authlevel "Anonymous"
```

> Ou crie diretamente pelo Visual Studio selecionando o template **Azure Functions** com gatilho HTTP e nível de autorização **Anonymous**.

---

### 2. Instalar o Pacote OpenAPI

Para habilitar a documentação Swagger/OpenAPI, instale o pacote:

```bash
dotnet add package Microsoft.Azure.Functions.Worker.Extensions.OpenApi
```

---

### 3. Substituir o Código da Função

Cole o seguinte código na classe `ConversaoTemperatura.cs`:

```csharp
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
```

---

## 🧪 Teste Local com Azurite

Para evitar custos com recursos do Azure, você pode simular uma conta de armazenamento local com o **Azurite**:

### Instalar Azurite

```bash
npm install -g azurite
```

### Executar Azurite

```bash
azurite
```

### Configurar `local.settings.json`

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  }
}
```

---

## ▶️ Executar o Projeto

Pressione **F5** no Visual Studio ou execute:

```bash
func start
```

Ao iniciar, o runtime exibirá os endpoints da função e da documentação Swagger.

---

## 🌐 Acessar Swagger UI

Abra o navegador e acesse:

```
http://localhost:7071/api/swagger/ui
```

Você verá uma interface gerada automaticamente com base nas definições OpenAPI da função.

---

## 🧪 Testar a Conversão

1. Na Swagger UI, selecione:
   ```
   GET > conversao-fahrenheit-para-celsius/{fahrenheit}
   ```

2. Insira um valor de temperatura em Fahrenheit (ex: `98.6`).

3. Clique em **Execute**.

4. O resultado da conversão será exibido abaixo, como:

```
O valor em fahrenheit 98.6 em celsius é 37.00
```

---

## 📦 Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [Azure Functions Core Tools](https://learn.microsoft.com/azure/azure-functions/functions-run-local)
- [Node.js + npm](https://nodejs.org/) (para Azurite)

---
https://learn.microsoft.com/pt-br/azure/azure-functions/openapi-apim-integrate-visual-studio?tabs=isolated-process


