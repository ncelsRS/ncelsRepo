using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Teme.Contract.Infrastructure.Workflow;
using Teme.Contract.Infrastructure.Workflow.ContractCoz;
using Teme.Contract.Infrastructure.Workflow.Payment;

namespace Teme.Contract.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWorkFlowInfrastructure(this IServiceCollection service)
        {
            service.AddTransient<SendToNcels>();
            service.AddTransient<SelectExecutorsFirst>();
            service.AddTransient<CozExecutorMeetReq>();
            service.AddTransient<CozExecutorNotMeetReq>();

            service.AddTransient<CozReturnToDeclarant>();

            service.AddTransient<CozBossMeetReq>();
            service.AddTransient<CozBossNotMeetReq>();

            service.AddTransient<CeoMeetReq>();
            service.AddTransient<CeoNotMeetReq>();
            service.AddTransient<RegisterContract>();

            service.AddTransient<SendPaymentToNcels>();
            service.AddTransient<SelectPaymentExecutors>();
            service.AddTransient<GvExecutorMeet>();
            service.AddTransient<GvExecutorNotMeet>();
            service.AddTransient<GvBossMeet>();
            service.AddTransient<GvBossNotMeet>();
            service.AddTransient<DefExecutorMeet>();
            service.AddTransient<DefExecutorNotMeet>();
            service.AddTransient<RegisterPayment>();

        }
    }
}
