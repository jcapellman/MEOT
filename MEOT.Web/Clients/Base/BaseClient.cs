using System.Net.Http;

namespace MEOT.Web.Clients.Base
{
    public class BaseClient
    {
        private string url = "https://api.meot.org/api/";

        protected readonly HttpClient http;

        protected string BuildURL(string endpoint) => $"{url}{endpoint}";

        protected BaseClient(HttpClient httpClient) => http = httpClient;
    }
}