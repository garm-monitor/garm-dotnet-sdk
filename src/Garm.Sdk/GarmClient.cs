using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Garm.Sdk
{
    public class GarmClient
    {
        private readonly HttpClient _httpClient;
        private readonly GarmOptions _options;

        public GarmClient(HttpClient httpClient, GarmOptions options)
        {
            _httpClient = httpClient;
            _options = options;

            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(_options.BaseUrl);
            }

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Garm-DotNet-SDK/1.0");
        }

        
        public async Task<bool> LogAsync(string level, string message, object? payload = null)
        {
            try
            {
                if (string.IsNullOrEmpty(_options.Token)) return false;

                var logEntry = new
                {
                    level = level,
                    message = message,
                    payload = EnrichPayload(payload)
                };

                var json = JsonSerializer.Serialize(logEntry);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, "/api/logs");
                request.Headers.Add("X-Garm-Token", _options.Token);
                request.Content = content;

                var response = await _httpClient.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // --- Helpers ---

        public Task<bool> InfoAsync(string message, object? payload = null) => LogAsync("info", message, payload);
        public Task<bool> WarningAsync(string message, object? payload = null) => LogAsync("warning", message, payload);
        public Task<bool> ErrorAsync(string message, object? payload = null) => LogAsync("error", message, payload);
        public Task<bool> CriticalAsync(string message, object? payload = null) => LogAsync("critical", message, payload);

        private object EnrichPayload(object? userPayload)
        {
            try 
            {
                return new
                {
                    _meta = new
                    {
                        dotnet = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription,
                        os = System.Runtime.InteropServices.RuntimeInformation.OSDescription,
                        hostname = System.Net.Dns.GetHostName(),
                        timestamp = DateTime.UtcNow
                    },
                    // Se userPayload for nulo, cria um objeto vazio
                    data = userPayload ?? new { }
                };
            }
            catch
            {
                return new { data = userPayload ?? new { } };
            }
        }
    }
}