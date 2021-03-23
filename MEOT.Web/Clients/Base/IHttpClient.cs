using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MEOT.Web.Clients.Base
{
    public class IHttpClient
    {
        private readonly HttpClient http;
        
        protected IHttpClient(HttpClient httpClient) => http = httpClient;

        protected async Task<T> GetAsync<T>(string url)
        {
            try
            {
                return await http.GetFromJsonAsync<T>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                throw;
            }
        }
    }
}