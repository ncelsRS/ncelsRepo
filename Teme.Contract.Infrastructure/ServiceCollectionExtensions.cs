using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Teme.Contract.Data;
using Teme.Contract.Infrastructure.Workflow;

namespace Teme.Contract.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWorkFlowInfrastructure(this IServiceCollection service)
        {
            service.AddTransient<SendToNcels>();
        }
    }
}
