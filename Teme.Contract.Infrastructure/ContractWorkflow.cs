using Serilog;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure.ContractCoz;
using Teme.Contract.Infrastructure.ContractGv;
using Teme.Contract.Infrastructure.WorkflowSteps;
using Teme.Shared.Data.Primitives.Contract;
using WorkflowCore.Interface;
using WorkflowCore.Models;

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
                .UserTask("Filling contract", data => data.ExecutorId)
                    .WithOption("sendWithoutSign", "sendWithoutSign").Do(then => then
                        .StartWith<SendWithoutSign>()
                    )
                    .WithOption("sendWithSign", "sendWithSign").Do(then => then
                        .StartWith<SendWithSign>()
                    )
                .If(d => d.ContractType == ContractTypeEnum.OneToOne).Do(then => then.ContractGv())
                .If(d => d.ContractType == ContractTypeEnum.OneToMore).Do(then => then.ContractCoz())
                .Then(context =>
                {
                    Log.Information("Workflow finished");
                    return ExecutionResult.Next();
                }); // TODO for disable errors
        }
    }

}
