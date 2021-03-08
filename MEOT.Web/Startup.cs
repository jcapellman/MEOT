using MEOT.lib.DAL;
using MEOT.lib.DAL.Base;
using MEOT.lib.Managers;
using MEOT.lib.Objects;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}