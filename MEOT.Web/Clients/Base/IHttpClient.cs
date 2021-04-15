using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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

        protected async Task<bool> PostAsync<T>(string url, T item)
        {
            try
            {
                var data = new StringContent(System.Text.Json.JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");
                
                var response = await http.PostAsync(url, data);

                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                throw;
            }
        }
    }
}