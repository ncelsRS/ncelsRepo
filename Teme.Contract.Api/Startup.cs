using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Reflection;
using NSwag.AspNetCore;
using Teme.Contract.Api.Startups;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Workflow;
using Teme.Shared.Data.Context;
using WorkflowCore.Interface;
using Teme.SharedApi;
using Teme.Shared.Data.Primitives.OrgScopes;
using System.Security.Cryptography.X509Certificates;

namespace Teme.Contract.Api
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

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // получаем строку подключения из файла конфигурации
            var connectionStr = Configuration.GetConnectionString("DefaultConnection");
            // добавляем контекст TemeContext в качестве сервиса в приложение
            services.AddDbContext<TemeContext>(options => options.UseSqlServer(connectionStr));
            services.AddCors();

            var certPath = Configuration["IdentityConfig:CertPath"];
            var certPass = Configuration["IdentityConfig:CertPass"];
            var cert = new X509Certificate2(certPath, certPass);
            services.AddRscAuth(Configuration, cert, new string[]
            {
                OrganizationScopeEnum.Ext
            });

            // Default vm template
            services.AddMvc();
            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<AutofacModule>();
            containerBuilder.RegisterInstance(Configuration);
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddSerilog();

            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings => { });

            app.UseRscAuth();

            // Start the Workflow instance
            app.UseContractInfrastructure();
            app.UseMvc();
        }
    }
}
