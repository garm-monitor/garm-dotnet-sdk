# 🐺 Garm Monitor .NET SDK

![.NET Version](https://img.shields.io/badge/.NET-Standard%202.0%20%7C%206%20%7C%208%20%7C%209-512bd4.svg?style=flat-square&logo=dotnet)
![License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)
![Garm Monitor](https://img.shields.io/badge/Garm-Official%20SDK-0D2538?style=flat-square)

O **SDK Oficial** para integração de aplicações .NET com o ecossistema de monitoramento **Garm Monitor**. Esta biblioteca permite o envio de logs estruturados e metadados de ambiente de forma assíncrona e resiliente.

---

## 🚀 Características

* **Multi-plataforma:** Suporte a .NET Standard 2.0 (compatível com .NET Framework 4.6.1+ e .NET Core/Moderno).
* **Injeção de Dependência:** Integração nativa com `IServiceCollection`.
* **Performance:** Utiliza `IHttpClientFactory` para gerenciamento eficiente de conexões.
* **Resiliência:** Fail-safe nativo para garantir que falhas no monitoramento nunca derrubem a aplicação principal.

## ⚙️ Configuração

No seu arquivo `Program.cs` (ou `Startup.cs`), adicione o SDK ao container de serviços:

```csharp
using Garm.Sdk;

// Registro do SDK
builder.Services.AddGarm(options => 
{
    options.Token = "seu_token_de_sistema_aqui";
    options.BaseUrl = "[https://api.garm-monitor.com.br](https://api.garm-monitor.com.br)"; // Ou seu endpoint local
});

```

## 📝 Como usar

O SDK fornece o serviço `GarmClient`, que pode ser injetado em qualquer classe através do construtor.

### Envio de Logs Simples

```csharp
public class MyBusinessService
{
    private readonly GarmClient _garm;

    public MyBusinessService(GarmClient garm)
    {
        _garm = garm;
    }

    public async Task ProcessOrder()
    {
        // ... lógica de negócio
        await _garm.InfoAsync("Pedido processado com sucesso.");
    }
}

```

### Envio de Logs com Payload (Dados Extras)

Você pode enviar objetos anônimos ou classes complexas como payload para facilitar o debug:

```csharp
await _garm.CriticalAsync("Falha na conexão com o Banco de Dados", new 
{ 
    Server = "10.0.0.50",
    RetryCount = 3,
    Exception = ex.Message 
});

```

## 🔍 Metadados Automáticos

O SDK captura automaticamente informações do ambiente para cada log enviado, incluindo:

* Versão do Runtime .NET
* Sistema Operacional
* Hostname da máquina
* Timestamp UTC

## 📂 Estrutura do Projeto

* `src/Garm.Sdk`: Código-fonte da biblioteca.
* `tests/Garm.Tester`: Aplicação de console para testes de integração.

---

Desenvolvido por [Carlos Miguel](https://www.linkedin.com/in/cg-alvaide/).

```

---

