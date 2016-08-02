using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StyleguideGenerator.Models.System;

namespace StyleguideGenerator
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            
            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {            
            this.ConfigureMvc(services);

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            
            
            //app.UseIISPlatformHandler(); 
            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute("Projects", "Projects/{action}/{name?}", new { controller = "Projects", action = "All" });
                routes.MapRoute("default", "{controller}/{action}/{id?}", new { controller = "Main", action = "Index" });
            });

        }

        private void ConfigureMvc(IServiceCollection services/*, ILoggerFactory logger*/)
        {
            var builder = services.AddMvc();
            // Setup global exception filters
            //builder.AddMvcOptions(o => { o.Filters.Add(new GlobalExceptionFilter()); });//logger
        }

    }
}
