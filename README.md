# 🌡️ Conversão de Temperatura com Azure Functions

Este projeto foi desenvolvido no curso da DIO: "Introdução a Deploy e Azure Functions" e demonstra como criar uma Azure Function do tipo **HTTP Trigger** com nível de segurança **Anonymous**, utilizando o modelo **in-process** e suporte à documentação via **OpenAPI/Swagger**.

---

## 📦 Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [Azure Functions Core Tools](https://learn.microsoft.com/azure/azure-functions/functions-run-local)
- [Node.js + npm](https://nodejs.org/) (para Azurite)

---

## 🚀 Para utilizar a aplicação, siga os passos recomendados abaixo

### 1. Clone o projeto

```bash
git clone https://github.com/erasmobezerra/AzureFunctionsConversaoTemperatura.git
cd ./AzureFunctionsConversaoTemperatura
```

---

### 2. Restaure os pacotes

```bash
dotnet restore
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
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated"
  }
}
```

---

## ▶️ Construe e Execute o Projeto

Pressione **F5** no Visual Studio ou execute:

```bash
dotnet build
func start
```

Ao iniciar, o runtime exibirá os endpoints da função e da documentação Swagger como no exemplo abaixo:

![alt text](image.png)

![alt text](image-1.png)

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

## 🤝 Como contribuir

1. Crie uma branch com nome descritivo: `feature/minha-mudanca`.  
2. Faça commits pequenos e claros.  
3. Abra Pull Request descrevendo o que foi alterado e por quê.  

---

🙏 Agradeço profundamente à **Digital Innovation One** por proporcionar este aprendizado gratuito e de qualidade. Um reconhecimento especial ao professor **[Leonardo Buta](https://www.linkedin.com/in/leonardo-buta/)** pela excelente didática e orientação durante todo o processo.

<div align="center">
  <p>⭐ Se este projeto foi útil para você, considere dar uma estrela!</p>
</div>
