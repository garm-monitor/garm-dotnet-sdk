using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Garm.Sdk
{
    public class GarmClient
    {   
        // 1. A Instância Única (Singleton) que o GarmExtensions procura
        public static GarmClient Instance { get; private set; }

        private readonly string _token;
        private readonly string _baseUrl;
        private static readonly HttpClient _httpClient = new HttpClient();

        // 2. Construtor PRIVADO: impede o 'new' fora desta classe
        private GarmClient(string token, string baseUrl)
        {
            _token = token;
            _baseUrl = baseUrl.TrimEnd('/');
            RegisterGlobalHandlers();
        }

        // 3. Inicialização (Boot)
        public static void Init(string token, string baseUrl = "http://localhost:8000/api")
        {
            if (Instance == null)
            {
                Instance = new GarmClient(token, baseUrl);
            }
        }

        // 4. Captura automática de erros
        private void RegisterGlobalHandlers()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) => {
                _ = SendLog("critical", $"Erro Fatal .NET: {e.ExceptionObject}");
            };
        }

        public async Task SendLog(string level, string message, object context = null)
        {
            if (string.IsNullOrEmpty(_token)) return;

            var payload = new {
                level = level.ToLower(),
                message = message,
                payload = new {
                    _meta = new { 
                        runtime = ".NET", 
                        os = Environment.OSVersion.ToString(), // Adicionado para o SOC
                        timestamp = DateTime.UtcNow 
                    },
                    custom_data = context
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            
            // Importante: no HttpClient compartilhado, evite limpar headers se houver concorrência
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/logs");
            request.Content = content;
            request.Headers.Add("X-Garm-Token", _token);

            try { await _httpClient.SendAsync(request); } catch { }
        }

        // 5. Atalhos Universais (Sintaxe: GarmClient.Critical)
        public static void Info(string m, object c = null) => _ = Instance?.SendLog("info", m, c);
        public static void Critical(string m, object c = null) => _ = Instance?.SendLog("critical", m, c);
    }
}