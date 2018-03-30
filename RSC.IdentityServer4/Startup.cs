using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RSC.IdentityServer4.IdentityConfig;
using Serilog;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Teme.Shared.Data.Context;

namespace RSC.IdentityServer4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // �������� ������ ����������� �� ����� ������������
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // ��������� �������� TemeContext � �������� ������� � ����������
            services.AddDbContext<TemeContext>(options => options.UseSqlServer(connection));
            services.AddMvc();

            var cert = new X509Certificate2("./IdentityConfig/identity.pfx", "ncels");

            var builder = services.AddIdentityServer()
                .AddSigningCredential(cert)
                .AddInMemoryIdentityResources(IdSrvConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdSrvConfig.GetApiResources())
                .AddInMemoryClients(IdSrvConfig.GetClients());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();

                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.Use((http, next) =>
                    {
                        return next();
                    });

            loggerFactory.AddSerilog();

            app.UseIdentityServer();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
