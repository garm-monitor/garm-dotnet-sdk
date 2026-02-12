using System;
using Garm.Sdk;
using Microsoft.Extensions.DependencyInjection;

// --- Configuração ---
var services = new ServiceCollection();

services.AddGarm(options =>
{
    // 👇 Certifique-se de usar um Token válido do seu banco local
    options.Token = "SEU_TOKEN_VÁLIDO_AQUI"; 
    options.BaseUrl = "http://localhost:8000/api"; 
});

var provider = services.BuildServiceProvider();

// --- Execução ---
Console.WriteLine("🐺 Iniciando Teste do Garm SDK .NET...");

var garm = provider.GetRequiredService<GarmClient>();

try
{
    Console.Write("Enviando log... ");
    
    // Ajuste de sintaxe para evitar o erro do print
    var payload = new 
    { 
        Driver = "Postgres",
        Query = "SELECT * FROM users",
        Detalhe = "Connection Timeout"
    };

    bool sucesso = await garm.CriticalAsync("Erro Crítico via C#", payload);

    if (sucesso)
    {
        Console.WriteLine("✅ SUCESSO! Verifique o Dashboard.");
    }
    else
    {
        Console.WriteLine("❌ FALHA! O servidor recusou ou está offline.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"\n❌ ERRO NO TESTE: {ex.Message}");
}