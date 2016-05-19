using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StyleguideGenerator.Infrastructure;

namespace StyleguideGenerator
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {            
            this.ConfigureMvc(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseExceptionHandler("/error");
            //app.UseIISPlatformHandler(); 
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action}/{id?}", new { controller = "Main", action = "Index" });
            });
        }

        private void ConfigureMvc(IServiceCollection services/*, ILoggerFactory logger*/)
        {
            var builder = services.AddMvc();
            // Setup global exception filters
            builder.AddMvcOptions(o => { o.Filters.Add(new GlobalExceptionFilter()); });//logger
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
