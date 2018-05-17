using System;
using System.Collections.Generic;
using System.Text;
using Teme.Contract.Infrastructure.Primitives;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.Workflow.Payment
{
    /// <summary>
    /// Workflow для Заявки на платеж
    /// </summary>
    public class PaymentWorkflow : IWorkflow<ContractWorkflowTransitionData>
    {
        public string Id => "Payment";

        public int Version => 0;

        public void Build(IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder.StartWith(c => { });            
        }
    }
}
