namespace Garm.Sdk;

public class GarmOptions
{
    // O Token é obrigatório
    public string Token { get; set; } = string.Empty;

    // A URL padrão é a do seu SaaS
    public string BaseUrl { get; set; } = "https://api.garm-monitor.com.br";

    // Timeout de 3 segundos para não travar o site do cliente
    public int TimeoutSeconds { get; set; } = 3;
}