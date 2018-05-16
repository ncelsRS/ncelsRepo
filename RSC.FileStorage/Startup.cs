using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;
using RSC.FileStorage.Startups;
using Serilog;
using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Teme.Shared.Data.Primitives.OrgScopes;
using Teme.SharedApi;

namespace RSC.FileStorage
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
            services.AddMvc();

            var certPath = Configuration["IdentityConfig:CertPath"];
            var certPass = Configuration["IdentityConfig:CertPass"];

            var cert = new X509Certificate2(certPath, certPass);
            services.AddRscAuth(Configuration, cert, new string[]
            {
                OrganizationScopeEnum.Identity
            });

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
            }

            loggerFactory.AddSerilog();

            #region CORS
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
                .AllowCredentials()
            );
            #endregion

            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings => { });

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
