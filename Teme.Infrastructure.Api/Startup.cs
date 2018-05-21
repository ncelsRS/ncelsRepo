using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;
using Serilog;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Workflow;
using Teme.Contract.Infrastructure.Workflow.Payment;
using Teme.Declaration.Infrastructure.TransitionDatas;
using Teme.Declaration.Infrastructure.Workflow;
using Teme.Infrastructure.Api.Startups;
using Teme.Shared.Data.Context;
using WorkflowCore.Interface;

namespace Teme.Infrastructure.Api
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
            services.AddDbContext<TemeContext>(options => options.UseSqlServer(connectionStr));

            // Add Workflow with the persistence provider
            services.AddCors();
            // Default vm template
            services.AddMvc();
            services.AddWorkflow(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), true, true));
            services.AddWorkFlowInfrastructure();

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
            // Start the Workflow instance
            var host = app.ApplicationServices.GetService<IWorkflowHost>();
            host.RegisterWorkflow<ContractWorkflow, ContractWorkflowTransitionData>();
            host.RegisterWorkflow<PaymentWorkflow, PaymentTransitionData>();
            host.RegisterWorkflow<DeclarationWorkflow, DeclarationTransitionData>();
            host.Start();
            app.UseMvc();
        }
    }
}
