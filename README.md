# 🐺 Garm Monitor .NET SDK

![.NET Version](https://img.shields.io/badge/.NET-Standard%202.0%20%7C%206%20%7C%208%20%7C%209-512bd4.svg?style=flat-square&logo=dotnet)
![License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)
![Garm Monitor](https://img.shields.io/badge/Garm-Official%20SDK-0D2538?style=flat-square)

O SDK Oficial para integração de aplicações .NET com o ecossistema Garm Monitor. Projetado para ser resiliente, assíncrono e universal, permitindo monitoramento em tempo real com impacto zero na performance da aplicação principal.

---

# 🚀 Instalação

Adicione o pacote ao seu projeto (via NuGet):
```bash
dotnet add package Garm.sdk
```

## ⚙️ Configuração(Boot)

O Garm utiliza o padrão Singleton. Configure uma única vez no início da sua aplicação (Program.cs) para ativar o monitoramento global e o "Vigia" de exceções.

```csharp
using Garm.Sdk;

// Inicializa o SDK
GarmClient.Init("SEU_TOKEN_DE_SISTEMA", "https://api.garm-monitor.com.br/api");

```


# 🐺 Uso Universal (Sintaxe Simplificada)

Após a inicialização, você não precisa injetar classes em todos os lugares. Use o acesso estático para logs rápidos em qualquer parte do código.

```csharp
// Envio Simples
GarmClient.Info("Usuário realizou login");

// Envio Crítico com Dados Extras (Payload)
GarmClient.Critical("Falha na integração de pagamento", new { 
    OrderId = 1050, 
    Gateway = "Stripe",
    Attempt = 3 
});

```

# 🏗️ Integração com Injeção de Dependência (ASP.NET Core)

Para projetos modernos que utilizam o container nativo do .NET, o SDK oferece suporte total:

```csharp
// No Program.cs
builder.Services.AddGarmMonitor("SEU_TOKEN_AQUI");

// No seu Controller ou Service
public class OrderService {
    private readonly GarmClient _garm;
    public OrderService(GarmClient garm) => _garm = garm;

    public async Task Process() {
        await _garm.SendLog("info", "Processando pedido...");
    }
}

```

# 🛡️ Monitoramento Automático (Vigia)

Ao inicializar o SDK, o Garm ativa o monitoramento passivo:

- Unhandled Exceptions: Captura automática de exceções que derrubariam o App.

- Metadados de SOC: Cada log é enriquecido automaticamente com Versão do Runtime e Sistema Operacional (OS).

- Async Nativo: O envio é feito em background, garantindo que o usuário não sinta lentidão.

# 📊 Níveis de Log

- Info(): Eventos informativos de rotina.

- Warning(): Situações de atenção.

- Error(): Falhas em processos específicos.

- Critical(): Alerta imediato via Webhook (Discord/Slack) configurado no painel.



Desenvolvido por [Carlos Miguel](https://www.linkedin.com/in/cg-alvaide/).

