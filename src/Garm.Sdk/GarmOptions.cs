namespace Garm.Sdk
{
    public class GarmOptions
    {
        public string BaseUrl { get; set; } = "http://127.0.0.1:8000/api";
        public int TimeoutSeconds { get; set; } = 2;
        public bool Enabled { get; set; } = true;
    }
}