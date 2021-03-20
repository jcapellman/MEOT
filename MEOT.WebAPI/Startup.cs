using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using MEOT.lib.DAL;
using MEOT.lib.Managers;
using MEOT.lib.Objects;

namespace MEOT.WebAPI
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
            services.AddControllers();

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MEOT API", Version = "v1" });
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MEOT API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}