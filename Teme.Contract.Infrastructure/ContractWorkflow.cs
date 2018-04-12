using System;
using Teme.Contract.Infrastructure.ContractCoz;
using Teme.Contract.Infrastructure.WorkflowSteps;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Infrastructure
{
    public class ContractWorkflow : IWorkflow<ContractWorkflowTransitionData>
    {
        public string Id => "Contract";

        public int Version => 0; // Test version

        public void Build(IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder
                .StartWith<SetWorkflowId>()
                    .Input(step => step.AwaiterKey, data => data.AwaiterKey)
                // Отправка в ЦОЗ
                .UserTask("Filling contract", data => "ExecutorId")
                    .WithOption("0", "SendWithoutSign").Do(then => then
                        .StartWith<SendWithoutSign>()
                            .Input(step => step.AwaiterKey, data => data.AwaiterKey)
                            .Output(step => step.ContractType, data => 1)
                    )
                    .WithOption("1", "SendWithSign").Do(then => then
                        .StartWith<SendWithSign>()
                            .Input(step => step.AwaiterKey, data => data.AwaiterKey)
                    )
                // "ContractType == 0" Мультизаявка
                .If(data => data.ContractType == 0).Do(x => x
                    .ContractCoz()
                )
                .If(data => data.ContractType == 1).Do(x => x
                    .ContractCoz()
                )
                .Then(context => ExecutionResult.Next());
        }
    }
}
