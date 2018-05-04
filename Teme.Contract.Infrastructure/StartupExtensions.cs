using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Workflow;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddContractInfrastructure(this IServiceCollection services)
        {
            services.AddWorkflow();

            return services;
        }

        public static IApplicationBuilder UseContractInfrastructure(this IApplicationBuilder app)
        {
            var host = app.ApplicationServices.GetService<IWorkflowHost>();
            host.RegisterWorkflow<ContractWorkflow, ContractWorkflowTransitionData>();

            return app;
        }
    }
}
