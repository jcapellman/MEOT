using Blazored.LocalStorage;
using MEOT.lib.DAL;
using MEOT.lib.DAL.Base;
using MEOT.lib.Managers;
using MEOT.lib.Objects;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MEOT.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            var db = new LiteDBDAL();   // Swap out this line if another database is preferred

            var settings = db.SelectFirstOrDefault<Settings>();

            var sourceManager = new SourceManager(settings);

            var settingsManager = new SettingsManager(db);

            settingsManager.UpdateSources(sourceManager.SourceNames);
            
            services.AddSingleton(sourceManager);
            services.AddSingleton(new MalwareManager(db));
            services.AddSingleton(settingsManager);
            services.AddSingleton(new TrendingAnalysisManager(db));
            services.AddSingleton(new UserManager(db));

            services.AddAuthorizationCore();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}