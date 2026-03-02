using System;
using Garm.Sdk; // Importa o seu SDK

class Program
{
    // Mude para static async Task
    static async Task Main(string[] args) 
    {
        GarmClient.Init("SPvWK8KPVhYE0MmAMbWne1hZexdGApkJOdq8Ra5YFfXvcONhBmWKu31Qd90H", "http://localhost:8000/api");

        Console.WriteLine("Enviando log do .NET...");

        // Use AWAIT para garantir que o programa espere o envio terminar
        await GarmClient.Instance.SendLog("critical", "Teste SDK .NET Real!");

        Console.WriteLine("Log enviado! Verifique o Dashboard.");
    }
}