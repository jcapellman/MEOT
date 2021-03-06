using System;
using System.Threading.Tasks;

using MEOT.lib.Common;

using MEOT.Web.Clients;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace MEOT.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            
            builder.Services.AddHttpClient<APIHttpClient>(client =>
                client.BaseAddress = new Uri(Constants.API_URL));
            
            await builder.Build().RunAsync();
        }
    }
}