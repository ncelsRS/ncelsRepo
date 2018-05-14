using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Teme.Contract.Data;
using Teme.Contract.Infrastructure.Workflow;
using Teme.Contract.Infrastructure.Workflow.ContractCoz;

namespace Teme.Contract.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWorkFlowInfrastructure(this IServiceCollection service)
        {
            service.AddTransient<SendToNcels>();
            service.AddTransient<SelectExecutorsFirst>();
            service.AddTransient<CozBossMeetReq>();
        }
    }
}
