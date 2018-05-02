using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NSwag.AspNetCore;
using RSC.IdentityServer.Startups;
using Serilog;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.OrgScopes;
using Teme.SharedApi;

namespace RSC.IdentityServer
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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // получаем строку подключения из файла конфигурации
            var connectionStr = Configuration.GetConnectionString("DefaultConnection");
            // добавляем контекст TemeContext в качестве сервиса в приложение
            services.AddDbContext<TemeContext>(options =>
            {
                options.UseSqlServer(connectionStr);
            });

            services.AddRscAuth(Configuration, new string[]
            {
                OrganizationScopeEnum.Ext
            });

            services.AddMvc();

            var containerBuilder = new Autofac.ContainerBuilder();
            containerBuilder.RegisterModule<AutofacModule>();
            containerBuilder.Populate(services);
            containerBuilder.RegisterInstance(Configuration);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
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

            loggerFactory.AddSerilog();

#if !DEBUG
            var urls = Configuration
                .GetChildren()
                .FirstOrDefault(x => x.Key == "Urls")
                .GetChildren()
                .Select(x => x.Value)
                .ToArray();
#endif
            app.UseCors(cfg => cfg
#if DEBUG
                .AllowAnyOrigin()
#else
                .WithOrigins(urls)
#endif
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings => { });

            app.UseAuthentication();

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